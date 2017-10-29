using System;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    /// <summary>
    /// Sends a Welcome PM when a user joins the server for the first time.
    /// </summary>
    public class WelcomeMessageHandler : Handler<IUserJoinsServerHandler>, IUserJoinsServerHandler
    {
        private readonly IMessageStrategy _messageStrategy;

        public WelcomeMessageHandler(IMessageStrategy messageStrategy)
        {
            _messageStrategy = messageStrategy;
        }

        public override void Init(IDiscloseFacade disclose, IDataStore dataStore)
        {
            _messageStrategy.Init(dataStore);

            base.Init(disclose, dataStore);
        }

        public async Task Handle(DiscloseUser user, DiscloseServer server)
        {
            string welcomeMessage = await _messageStrategy.GetWelcomeMessage(server);

            await Disclose.SendMessageToUser(user, welcomeMessage);
        }
    }
}
