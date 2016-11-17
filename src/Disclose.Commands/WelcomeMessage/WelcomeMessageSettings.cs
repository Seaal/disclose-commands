using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    public class WelcomeMessageSettings
    {
        public IMessageStrategy MessageStrategy { get; set; }

        public bool AllowDisplayCommand { get; set; }

        public Func<IUser, bool> SetUserFilter { get; set; }

        public WelcomeMessageSettings()
        {
            MessageStrategy = new StringMessageStrategy("Welcome to the server!");
            AllowDisplayCommand = true;
        }
    }
}
