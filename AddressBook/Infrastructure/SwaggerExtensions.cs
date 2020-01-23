using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace AddressBook.Api.Infrastructure
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Address Book API",
                    Version = "v1.1",
                    Description = "Address Book API"
                });
            });

            return services;
        }
    }
}
