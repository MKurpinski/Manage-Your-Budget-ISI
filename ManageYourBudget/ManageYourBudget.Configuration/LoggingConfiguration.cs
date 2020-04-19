using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ManageYourBudget.Configuration
{
    public static class LoggingConfiguration
    {
        public static void EnableSerilog(this ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
        }
    }
}
