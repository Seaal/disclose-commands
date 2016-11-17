using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    public class SetWelcomeMessageCommandHandler : CommandHandler
    {
        private readonly IMessageStrategy _messageStrategy;

        public SetWelcomeMessageCommandHandler(IMessageStrategy messageStrategy)
        {
            _messageStrategy = messageStrategy;
        }

        public override string CommandName => "setwelcome";
        public override string Description => "Update the Welcome Message received by users who first join the server";

        public override async Task Handle(IMessage message, string arguments)
        {
            if (String.IsNullOrWhiteSpace(arguments))
            {
                await Discord.SendMessageToUser(message.User, "No Welcome Message provided");
            }
            else
            {
                await _messageStrategy.SetWelcomeMessage(Disclose.Server, arguments.Trim());

                await Discord.SendMessageToUser(message.User, "Welcome Message successfully set");
            }
        }
    }
}
