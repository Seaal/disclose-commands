using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    public class StringMessageStrategy : IMessageStrategy
    {
        private readonly string _message;

        public bool HasSetCommand => false;

        public StringMessageStrategy(string message)
        {
            _message = message;
        }

        public Task<string> GetWelcomeMessage(IServer server)
        {
            return Task.FromResult(_message);
        }

        public Task SetWelcomeMessage(IServer server, string welcomeMessage)
        {
            return Task.FromResult(0);
        }

        public void Init(IDataStore dataStore)
        {
        }
    }
}
