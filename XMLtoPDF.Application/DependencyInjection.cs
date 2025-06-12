using Microsoft.Extensions.DependencyInjection;
using System.Xml;
using XMLtoPDF.Application.Services;
using XMLtoPDF.Application.Utilities;

namespace XMLtoPDF.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<XmlResolver, CustomDtdResolver>();

            return services;
        }
    }
}
