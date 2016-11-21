using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.Gamble
{
    public class GambleOptions
    {
        public string Currency { get; set; }
        public string CurrencySingular { get; set; }
        public int DefaultAmount { get; set; }
        public int ResetAmount { get; set; }
        public TimeSpan? ResetCooldown { get; set; }
    }
}
