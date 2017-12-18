using System;
using System.Threading.Tasks;

namespace Disclose.Commands.Currency
{
    internal class UseCurrencyDecorator : ICommandHandler
    {
        private readonly ICommandHandler _commandHandler;

        private readonly int _cost;
        private readonly CurrencySettings _settings;

        private IDataStore _dataStore;
        private IDiscloseFacade _disclose;

        public UseCurrencyDecorator(ICommandHandler commandHandler, int cost, CurrencySettings currencySettings)
        {
            _commandHandler = commandHandler;
            _cost = cost;
            _settings = currencySettings;
        }

        public string CommandName => _commandHandler.CommandName;

        public string Description => _commandHandler.Description;

        public Func<DiscloseChannel, bool> ChannelFilter => _commandHandler.ChannelFilter;

        public Func<DiscloseUser, bool> UserFilter => _commandHandler.UserFilter;

        public async Task Handle(DiscloseMessage message, string arguments)
        {
            int userTotal = await _dataStore.GetUserDataAsync<int>(message.User, _settings.DataStoreKey);

            if(userTotal < _cost)
            {
                await _disclose.SendMessageToUser(message.User, $"You do not have enough {_settings.CurrencyName}.");

                return;
            }
            else
            {
                userTotal -= _cost;

                await _dataStore.SetUserDataAsync(message.User, _settings.DataStoreKey, userTotal);
            }

            await _commandHandler.Handle(message, arguments);

            await _disclose.SendMessageToUser(message.User, $"You now have {userTotal} {_settings.CurrencyName}.");
        }

        public void Init(IDiscloseFacade disclose, IDataStore dataStore)
        {
            _disclose = disclose;
            _dataStore = dataStore;

            _commandHandler.Init(disclose, dataStore);
        }

        public ICommandHandler RestrictedToChannels(Func<DiscloseChannel, bool> channel)
        {
            return _commandHandler.RestrictedToChannels(channel);
        }

        public ICommandHandler RestrictToUsers(Func<DiscloseUser, bool> user)
        {
            return _commandHandler.RestrictToUsers(user);
        }
    }
}
