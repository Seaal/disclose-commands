using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disclose.Commands.Currency
{
    public static class CurrencyHelper
    {
        public static string GetCurrencyName(ulong amount, CurrencySettings settings)
        {
            if (amount == 1)
            {
                return settings.CurrencyNameSingular;
            }

            return settings.CurrencyName;
        }
    }
}
