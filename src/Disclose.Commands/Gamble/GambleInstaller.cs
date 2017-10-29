using Disclose.DiscordClient.DiscordNetAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.Gamble
{
    public class GambleInstaller : IInstaller
    {
        private readonly GambleInstallerOptions _options;

        public GambleInstaller()
        {
            _options = new GambleInstallerOptions();
        }

        public GambleInstaller(Action<GambleInstallerOptions> setOptions) : this()
        {
            setOptions(_options);
        }

        public void Install(DiscloseClient discoseClient)
        {
            ICommandHandler gambleCommandHandler = new GambleCommandHandler(_options)
                                                   .RestrictedToChannels(_options.ChannelRestrictions);

            discoseClient.Register(gambleCommandHandler);
        }
    }
}
