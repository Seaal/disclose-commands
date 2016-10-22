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

        public StringMessageStrategy(string message)
        {
            _message = message;
        }

        public Task<string> GetWelcomeMessage(IServer server)
        {
            return Task.FromResult(_message);
        }

        public void Init(IDataStore dataStore)
        {
        }
    }
}
