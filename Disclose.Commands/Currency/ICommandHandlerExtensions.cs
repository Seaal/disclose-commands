using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disclose.Commands.Currency
{
    public static class ICommandHandlerExtensions
    {
        public static ICommandHandler UseCurrency(this ICommandHandler commandHandler, int cost)
        {
            return UseCurrency(commandHandler, cost, new CurrencySettings());
        }

        public static ICommandHandler UseCurrency(this ICommandHandler commandHandler, int cost, CurrencySettings settings)
        {
            return new UseCurrencyDecorator(commandHandler, cost, settings);
        }
    }
}
