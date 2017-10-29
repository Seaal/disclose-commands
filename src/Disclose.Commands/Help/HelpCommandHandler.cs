using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.Help
{
    public class HelpCommandHandler : CommandHandler
    {
        public override string CommandName => "help";
        public override string Description => "Gives information about the commands available to use for this bot";

        public override async Task Handle(DiscloseMessage message, string arguments)
        {
            string helpMessage;

            if (String.IsNullOrWhiteSpace(arguments))
            {
                helpMessage = GetHelpForAllCommands(message.User);
            }
            else
            {
                ICommandHandler commandHandler = Disclose.CommandHandlers.FirstOrDefault(ch => ch.CommandName == arguments.ToLowerInvariant());

                helpMessage = GetHelpForCommand(commandHandler, message.User) ?? "Command does not exist";
            }

            await Disclose.SendMessageToUser(message.User, helpMessage);
        }

        private string GetHelpForAllCommands(DiscloseUser user)
        {
            string helpMessage = "Here are the commands available for you:\n\n";

            foreach (ICommandHandler commandHandler in Disclose.CommandHandlers)
            {
                string commandHelpMessage = GetHelpForCommand(commandHandler, user);

                if (commandHelpMessage != null)
                {
                    helpMessage += $"{commandHelpMessage}\n";
                }
            }

            return helpMessage;
        }

        private string GetHelpForCommand(ICommandHandler commandHandler, DiscloseUser user)
        {
            if (commandHandler != null && (commandHandler.UserFilter == null || commandHandler.UserFilter(user)))
            {
                return $"**{commandHandler.CommandName}** - {commandHandler.Description}";
            }

            return null;
        }
    }
}
