using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disclose.Commands.Currency
{
    public class CurrencySettings
    {
        public string CurrencyName { get; set; }
        public string CurrencyNameSingular { get; set; }
        public string DataStoreKey { get; set; }

        public CurrencySettings()
        {
            CurrencyName = "Coins";
            CurrencyNameSingular = "Coin";
            DataStoreKey = "disclose-currency";
        }
    }
}
