using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    public class DisplayWelcomeMessageCommandHandler : CommandHandler
    {
        private readonly IMessageStrategy _messageStrategy;

        public DisplayWelcomeMessageCommandHandler(IMessageStrategy messageStrategy)
        {
            _messageStrategy = messageStrategy;
        }

        public override string CommandName => "displaywelcome";
        public override string Description => "Redisplays the welcome message first received when joining the server";

        public override async Task Handle(IMessage message, string arguments)
        {
            string welcomeMessage = await _messageStrategy.GetWelcomeMessage(Disclose.Server);

            if (welcomeMessage == null)
            {
                await Discord.SendMessageToUser(message.User, "No welcome message has been set.");
            }
            else
            {
                await Discord.SendMessageToUser(message.User, welcomeMessage);
            }
        }

        
    }
}
