using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.WelcomeMessage
{
    public class WelcomeMessageInstaller : IInstaller
    {
        private readonly IMessageStrategy _messageStrategy;


        public WelcomeMessageInstaller(IMessageStrategy messageStrategy)
        {
            _messageStrategy = messageStrategy;
        }

        public void Install(DiscloseClient discoseClient)
        {
            discoseClient.Register(new WelcomeMessageHandler(_messageStrategy));
        }
    }
}
