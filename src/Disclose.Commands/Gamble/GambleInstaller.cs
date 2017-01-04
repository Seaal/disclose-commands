using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.Gamble
{
    public class GambleInstaller : IInstaller
    {
        private readonly GambleOptions _options;

        public GambleInstaller()
        {
            _options = new GambleOptions();
        }

        public GambleInstaller(Action<GambleOptions> setOptions) : this()
        {
            setOptions(_options);
        }

        public void Install(DiscloseClient discoseClient)
        {
            discoseClient.Register(new GambleCommandHandler(_options));
        }
    }
}
