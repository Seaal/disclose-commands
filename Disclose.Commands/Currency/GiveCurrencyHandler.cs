using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Disclose.Commands.Currency.CurrencyHelper;

namespace Disclose.Commands.Currency
{
    public class GiveCurrencyHandler : CommandHandler
    {
        public override string CommandName => "give";

        public override string Description => $"Gives an amount of {_currencySettings.CurrencyName} to the user";

        private readonly CurrencySettings _currencySettings;
        private readonly Regex _messageRegex;

        public GiveCurrencyHandler(CurrencySettings currencySettings)
        {
            _currencySettings = currencySettings;
            _messageRegex = new Regex(@"(?:(?:<@(\d+)>)|(?:(\w+)(?:#(\d{4}))?))\s(\d+)", RegexOptions.Compiled);
        }

        public async override Task Handle(DiscloseMessage message, string arguments)
        {
            (bool success, Func<DiscloseUser, bool> userFilter, ulong amount) = ParseArguments(arguments);

            if (!success)
            {
                await Disclose.SendMessageToChannel(message.Channel, $"Invalid command format. Correct format is:\n{CommandName} @User <Amount>\nOR\n{CommandName} <Username>#<User number> <Amount>");
                return;
            }

            DiscloseUser user = (await Disclose.Server.GetUsersAsync()).FirstOrDefault(userFilter);

            if (user == null)
            {
                await Disclose.SendMessageToChannel(message.Channel, $"That user could not be found in this server.");
                return;
            }

            ulong currencyAmount = await DataStore.GetUserDataAsync<ulong>(user, _currencySettings.DataStoreKey);

            currencyAmount += amount;

            await DataStore.SetUserDataAsync(user, _currencySettings.DataStoreKey, currencyAmount);

            await Disclose.SendMessageToChannel(message.Channel, $"Gave {amount} {GetCurrencyName(amount, _currencySettings)} to {user.Name}.");
        }

        private (bool Success, Func<DiscloseUser, bool> UserFilter, ulong Amount) ParseArguments(string arguments)
        {
            Match match = _messageRegex.Match(arguments);

            if (!match.Success)
            {
                return (false, null, 0);
            }

            string amountString = match.Groups[4].Value;

            if(!ulong.TryParse(amountString, out ulong amount))
            {
                return (false, null, 0);
            }

            //If the second group matched, then it is a mentioned user
            if(match.Groups[1].Success)
            {
                string userIdString = match.Groups[1].Value;

                if(ulong.TryParse(userIdString, out ulong userId))
                {
                    return (true, (user) => user.Id == userId, amount);
                }
            }
            else
            {
                string username = match.Groups[2].Value;
                string discriminator = match.Groups[3].Value;

                return (true, (user) => user.Name == username && (discriminator == "" || true), amount);
            }        

            return (false, null, 0);
        }
    }
}
