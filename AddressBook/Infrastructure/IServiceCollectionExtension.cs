using AddressBook.Api.Hubs;
using AddressBook.BusinessLayer.AutoMapper;
using AddressBook.BusinessLayer.Services;
using AddressBook.DataAccessLayer.Repositories;
using AddressBook.Shared.Contracts.Business;
using AddressBook.Shared.Contracts.DataAccess;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace AddressBook.Api.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISettlementService, SettlementService>();
            services.AddScoped<IContactService, ContactService>();
            //services.AddSingleton<ContactDatabaseSubscription, ContactDatabaseSubscription>();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ISettlementRepository, SettlementRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
        }

        public static void AutoMapperConfig(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddUrlHelper(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x => {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
