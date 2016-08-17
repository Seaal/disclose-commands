using System;
using System.Threading.Tasks;
using Disclose.DiscordClient;

namespace Disclose.Commands.WelcomeMessage
{
    /// <summary>
    /// Sends a Welcome PM when a user joins the server for the first time.
    /// </summary>
    public class WelcomeMessageHandler : Handler, IUserJoinsServerHandler
    {
        public Task Handle(IUser user, IServer server)
        {
            throw new NotImplementedException();
        }
    }
}
