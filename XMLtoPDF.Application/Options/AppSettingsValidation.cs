using Microsoft.Extensions.Options;
using SE.GMO.Billing.Application.Options;
using SE.GMO.Billing.Application.Utilities;

namespace XMLtoPDF.Application.Options
{
    public class AppSettingsValidation : IValidateOptions<AppSettings>
    {
        public ValidateOptionsResult Validate(string? name, AppSettings options)
        {
            Dictionary<string, string> directories = new()
            {
                { "AppSettings.InputFolderXML", options.InputFolderXML },
                { "AppSettings.ProcessingFolderXML", options.ProcessingFolderXML },
                { "AppSettings.OutputFolderPDF", options.OutputFolderPDF },
                { "AppSettings.ErrorFolderXML", options.ErrorFolderXML },
            };

            foreach (KeyValuePair<string, string> entry in directories)
            {
                if (!Directory.Exists(entry.Value))
                {
                    return ValidateOptionsResult.Fail($"{entry.Key}: '{entry.Value}' directory was not found");
                }
            }

            Dictionary<string, string> cronExpressions = new()
            {
                { "AppSettings.PollingInputFolderXML", options.PollingInputFolderXML }
            };

            foreach (KeyValuePair<string, string> entry in cronExpressions)
            {
                if (!CronExpressionUtil.IsValidCronSchedule(entry.Value))
                {
                    return ValidateOptionsResult.Fail($"{entry.Key}: '{entry.Value}' is an invalid cron expression");
                }
            }

            return ValidateOptionsResult.Success;
        }
    }
}
