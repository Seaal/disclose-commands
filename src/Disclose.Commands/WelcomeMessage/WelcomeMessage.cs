using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.WelcomeMessage
{
    public static class WelcomeMessage
    {
        public static IInstaller Installer(WelcomeMessageSettings settings)
        {
            return new WelcomeMessageInstaller(settings);
        }
    }
}
