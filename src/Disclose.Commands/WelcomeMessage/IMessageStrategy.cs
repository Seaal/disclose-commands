﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Disclose.Commands.WelcomeMessage
{
    /// <summary>
    /// A strategy to choose where to get the welcome message from.
    /// </summary>
    public interface IMessageStrategy
    {
        Task<string> GetWelcomeMessage();
    }
}
