// reloadOnChange: false -> to force service to restart and apply validations to config values
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using SE.GMO.Billing.Application.Options;
using Serilog;
using XMLtoPDF.Application;
using XMLtoPDF.Application.Options;
using XMLtoPDF.Domain;
using XMLtoPDF.Infrastructure;
using XMLToPDF.FileProcessor.Jobs;

var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(configuration)
  .CreateLogger();

try
{
    var host = Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureServices((hostContext, services) =>
        {
            services.TryAddSingleton<IValidateOptions<AppSettings>, AppSettingsValidation>();

            services.AddOptions<AppSettings>()
                .BindConfiguration(nameof(AppSettings)) // Bind the section in config
                .ValidateOnStart(); // Compile time (when application starts) validation, especially on IOptions.

            services.AddApplicationServices();

            services.AddInfrastructureServices();

            services.AddDomainServices();

            services.AddQuartz(q =>
            {
                var serviceProvider = services.BuildServiceProvider();

                IOptions<AppSettings> appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>();

                q.AddJob<XmlProcessorJob>(opts => opts.WithIdentity("xmlProcessor"));
                q.AddTrigger(opts => opts
                    .ForJob("xmlProcessor")
                    .WithIdentity("xmlProcessorTrigger")
                    .StartNow()
                    .WithCronSchedule(appSettings.Value.PollingInputFolderXML));
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        });

    var app = host.Build();

    await app.RunAsync();
}
catch (Exception ex) when (ex.GetType().Name != "HostAbortedException") // https://goforgoldman.com/posts/ef-gotcha/
{
    Log.Logger.Fatal(ex, "Application start-up failed");
}
finally
{
    await Log.CloseAndFlushAsync();
}
