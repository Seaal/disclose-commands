using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    /// <summary>
    /// A strategy to choose where to get the welcome message from.
    /// </summary>
    public interface IMessageStrategy
    {
        bool HasSetCommand { get; }

        Task<string> GetWelcomeMessage(IServer server);

        Task SetWelcomeMessage(IServer server, string welcomeMessage);

        void Init(IDataStore dataStore);
    }
}
