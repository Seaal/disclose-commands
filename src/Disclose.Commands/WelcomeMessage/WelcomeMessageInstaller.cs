using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.WelcomeMessage
{
    public class WelcomeMessageInstaller : IInstaller
    {
        private readonly WelcomeMessageSettings _settings;

        public WelcomeMessageInstaller(WelcomeMessageSettings settings)
        {
            _settings = settings;
        }

        public void Install(DiscloseClient discoseClient)
        {
            discoseClient.Register(new WelcomeMessageHandler(_settings.MessageStrategy));

            if (_settings.AllowDisplayCommand)
            {
                discoseClient.Register(new DisplayWelcomeMessageCommandHandler(_settings.MessageStrategy));
            }

            if (_settings.MessageStrategy.HasSetCommand)
            {
                discoseClient.Register(new SetWelcomeMessageCommandHandler(_settings.MessageStrategy)
                                            .RestrictedToChannels(c => c.IsPrivateMessage)
                                            .RestrictToUsers(_settings.SetUserFilter));
            }
        }
    }
}
