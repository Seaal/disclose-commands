using Disclose.Commands.Currency;
using Disclose.DiscordClient;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Disclose.Commands.Tests.Currency
{
    public class GiveCurrencyHandlerTests
    {
        private readonly GiveCurrencyHandler _giveCurrencyHandler;
        private readonly DiscloseClient _discloseClient;
        private readonly IDataStore _dataStore;

        public GiveCurrencyHandlerTests()
        {
            _giveCurrencyHandler = new GiveCurrencyHandler(new CurrencySettings());

            _dataStore = Substitute.For<IDataStore>();

            _discloseClient = new DiscloseClient();

            _giveCurrencyHandler.Init(_disclose, _dataStore);
        }

        public class Handle : GiveCurrencyHandlerTests
        {
            [Fact]
            public async Task Should_Run()
            {
                DiscloseMessage message = Substitute.For<DiscloseMessage>(Substitute.For<IMessage>(), null);

                string arguments = "Seaal#1234 12345";

                await _giveCurrencyHandler.Handle(message, arguments);
            }
        }
    }
}
