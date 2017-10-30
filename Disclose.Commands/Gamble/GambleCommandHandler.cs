using System;
using System.Threading.Tasks;

namespace Disclose.Commands.Gamble
{
    public class GambleCommandHandler : CommandHandler
    {
        private readonly GambleOptions _options;
        private readonly Random _random;

        public GambleCommandHandler(GambleOptions options)
        {
            _options = options;
            _random = new Random();
            Description = $"Gamble an amount of {options.Currency} to potentially win more. If you run out, reset your {options.Currency} with the \"gamble reset\" command";
        }

        public override string CommandName => "gamble";
        public override string Description { get; }

        public override async Task Handle(DiscloseMessage message, string arguments)
        {
            if (DataStore == null)
            {
                throw new InvalidOperationException("Gamble requires a datastore to be used");
            }

            if (String.IsNullOrWhiteSpace(arguments))
            {
                GambleData storedData = await DataStore.GetUserDataAsync<GambleData>(message.User, "disclose-gamble") ?? new GambleData()
                {
                    Amount = _options.DefaultAmount
                };

                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> You have {FormatWithCurrency(storedData.Amount)}.");

                return;
            }

            arguments = arguments.ToLowerInvariant();

            int amountToBet;

            if (!int.TryParse(arguments, out amountToBet) && arguments != "reset")
            {
                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> Invalid Gamble input, either input a positive whole number to gamble or reset to reset your {_options.Currency}.");

                return;
            }

            if (amountToBet < 1 && arguments != "reset")
            {
                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> Invalid Gamble input, you must input a positive whole number to gamble.");

                return;
            }

            GambleData data = await DataStore.GetUserDataAsync<GambleData>(message.User, "disclose-gamble");

            if (arguments == "reset")
            {
                await ProcessReset(message, data);
            }
            else
            {
                await ProcessBet(message, data, amountToBet);
            }
        }

        private async Task ProcessReset(DiscloseMessage message, GambleData data)
        {
            if (data == null)
            {
                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> You cannot reset until you have played at least once.");
            }
            else if (data.ResetTime.HasValue && DateTime.Now < data.ResetTime.Value)
            {
                TimeSpan remainingResetCooldown = data.ResetTime.Value - DateTime.Now;

                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> You cannot reset right now. You can reset again in {FormatCooldownTime(remainingResetCooldown)}.");
            }
            else if (data.Amount < _options.ResetAmount)
            {
                data.Amount = _options.ResetAmount;

                if (_options.ResetCooldown.HasValue)
                {
                    data.ResetTime = DateTime.Now.Add(_options.ResetCooldown.Value);
                }

                await DataStore.SetUserDataAsync(message.User, "disclose-gamble", data);

                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> Your {_options.Currency} have been reset to {_options.ResetAmount}.");
            }
            else
            {
                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> You cannot reset right now, you have {FormatWithCurrency(data.Amount)}.");
            }
        }

        private async Task ProcessBet(DiscloseMessage message, GambleData data, int amountToBet)
        {
            if (data == null)
            {
                data = new GambleData()
                {
                    Amount = _options.DefaultAmount
                };
            }

            if (amountToBet > data.Amount)
            {
                await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> You cannot gamble that much. You only have {FormatWithCurrency(data.Amount)}.");
            }
            else
            {
                int randomNumber = _random.Next(100);

                if (randomNumber >= 50)
                {
                    data.Amount += amountToBet;
                    await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> Roll: {randomNumber}. You have won {FormatWithCurrency(amountToBet)} :money_mouth:. You now have {FormatWithCurrency(data.Amount)}.");
                }
                else
                {
                    data.Amount -= amountToBet;
                    await Disclose.SendMessageToChannel(message.Channel, $"<@{message.User.Id}> Roll: {randomNumber}. You have lost {FormatWithCurrency(amountToBet)} :face_palm:. You now have {FormatWithCurrency(data.Amount)}.");
                }

                await DataStore.SetUserDataAsync(message.User, "disclose-gamble", data);
            }
        }

        private string FormatWithCurrency(int amount)
        {
            string currency = amount == 1 ? _options.CurrencySingular : _options.Currency;

            return $"{amount} {currency}";
        }

        private string FormatCooldownTime(TimeSpan span)
        {
            string formatString = "";

            formatString += IncludeTimeElement(() => span.Days, formatString, "%d", "Day");
            formatString += IncludeTimeElement(() => span.Hours, formatString, "%h", "Hour");
            formatString += IncludeTimeElement(() => span.Minutes, formatString, "%m", "Minute");
            formatString += IncludeTimeElement(() => span.Seconds, formatString, "%s", "Second");

            return span.ToString(formatString);
        }

        private string IncludeTimeElement(Func<int> getTimeElement, string currentFormatString, string elementSpecifier, string elementName)
        {
            int timeElement = getTimeElement();

            string comma = currentFormatString == String.Empty ? "" : ", ";

            if (timeElement > 1)
            {
                return $"'{comma}'{elementSpecifier}' {elementName}s'";
            }

            if (timeElement == 1)
            {
                return $"'{comma}'{elementSpecifier}' {elementName}'";
            }

            return "";
        }
    }
}
