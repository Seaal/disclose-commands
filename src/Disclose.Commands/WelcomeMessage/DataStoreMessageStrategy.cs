using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    public class DataStoreMessageStrategy : IMessageStrategy
    {
        private IDataStore _dataStore;
        private readonly string _dataKey;

        public DataStoreMessageStrategy()
        {
            _dataKey = "disclose-welcomeMessage";
        }

        public DataStoreMessageStrategy(string dataKey)
        {
            _dataKey = dataKey;
        }

        public async Task<string> GetWelcomeMessage(IServer server)
        {
            if (_dataStore == null)
            {
                throw new InvalidOperationException("The DataStore must be set for the welcome message to be retrieved");
            }

            return await _dataStore.GetServerDataAsync<string>(server, _dataKey);
        }

        public void Init(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
    }
}
