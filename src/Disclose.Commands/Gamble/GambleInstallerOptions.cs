using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.Gamble
{
    public class GambleInstallerOptions : GambleOptions
    {
        public Func<DiscloseChannel, bool> ChannelRestrictions { get; set; }

        public GambleInstallerOptions()
        {
            ChannelRestrictions = null;
        }
    }
}
