using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.WelcomeMessage
{
    public class StringMessageStrategy : IMessageStrategy
    {
        private readonly string _message;

        public StringMessageStrategy(string message)
        {
            _message = message;
        }

        public Task<string> GetWelcomeMessage()
        {
            return Task.FromResult(_message);
        }
    }
}
