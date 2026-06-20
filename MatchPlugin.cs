using Microsoft.Xrm.Sdk;
using System;
using System.Text.RegularExpressions;

namespace customapilab
{
    /// <summary>
    /// Plugin development guide: https://docs.microsoft.com/powerapps/developer/common-data-service/plug-ins
    /// Best practices and guidance: https://docs.microsoft.com/powerapps/developer/common-data-service/best-practices/business-logic/
    /// </summary>
    public class MatchPlugin : PluginBase
    {
        public MatchPlugin(string unsecureConfiguration, string secureConfiguration)
            : base(typeof(MatchPlugin))
        {
            // TODO: Implement your custom configuration handling
            // https://docs.microsoft.com/powerapps/developer/common-data-service/register-plug-in#set-configuration-data
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            string input = (string)context.InputParameters["StringIn"];

            string pattern = (string)context.InputParameters["Pattern"];

            ITracingService tracingService = (ITracingService)localPluginContext.ServiceProvider.GetService(typeof(ITracingService));

            tracingService.Trace("Provided input: " + input);

            var result = Regex.Match(input, pattern);

            tracingService.Trace("Matching result: " + result.Success);

            context.OutputParameters["Matched"] = result.Success;
        }
    }
}
