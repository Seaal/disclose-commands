using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.Help
{
    public class HelpInstaller : IInstaller
    {
        public void Install(DiscloseClient discoseClient)
        {
            discoseClient.Register(new HelpCommandHandler());
        }
    }
}
