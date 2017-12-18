using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Disclose.Commands.Currency.CurrencyHelper;

namespace Disclose.Commands.Currency
{
    public class ShowCurrencyHandler : CommandHandler
    {
        public override string CommandName => "currency";

        public override string Description => $"Displays the amount of {_currencySettings.CurrencyName} you have.";

        private readonly CurrencySettings _currencySettings;

        public ShowCurrencyHandler(CurrencySettings currencySettings)
        {
            _currencySettings = currencySettings;
        }

        public async override Task Handle(DiscloseMessage message, string arguments)
        {
            ulong currencyAmount = await DataStore.GetUserDataAsync<ulong>(message.User, _currencySettings.DataStoreKey);

            await Disclose.SendMessageToChannel(message.Channel, $"You have {currencyAmount} {GetCurrencyName(currencyAmount, _currencySettings)}.");
        }
    }
}
