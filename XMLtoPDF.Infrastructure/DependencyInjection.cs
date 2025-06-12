using Microsoft.Extensions.DependencyInjection;
using XMLtoPDF.Application.Interfaces;
using XMLtoPDF.Infrastructure.Interfaces;
using XMLtoPDF.Infrastructure.Persistence.XMLtoPDF.Infrastructure.Data;
using XMLtoPDF.Infrastructure.Persistence;
using XMLtoPDF.Infrastructure.Services;

namespace XMLtoPDF.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IXmlValidator, XmlValidator>();
            services.AddScoped<IXmlFileLoader, XmlFileLoader>();
            
            services.AddSingleton<LiteDbContext>();
            services.AddScoped<IProcessedFileRepository, ProcessedFileRepository>();

            return services;
        }
    }

}
