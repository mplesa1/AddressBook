using AddressBook.Api.Infrastructure;
using AddressBook.BusinessLayer.AutoMapper;
using AddressBook.BusinessLayer.Services;
using AddressBook.DataAccessLayer.Persistence.Contexts;
using AddressBook.DataAccessLayer.Repositories;
using AddressBook.Shared.Contracts.Business;
using AddressBook.Shared.Contracts.DataAccess;
using AddressBook.Shared.Infrastructure.Middlerwares;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace AddressBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvcCore()
                    .AddApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddSwaggerDocumentation();

            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("databaseString"));
                options.EnableSensitiveDataLogging();
            }, ServiceLifetime.Transient);

            //services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<IUrlHelper>(factory =>
            //{
            //    var actionContext = factory.GetService<IActionContextAccessor>()
            //                                   .ActionContext;
            //    if (actionContext != null)
            //    {
            //        return new UrlHelper(actionContext);
            //    }
            //    return null;
            //});
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x => {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });

            #region Services DI
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ISettlementService, SettlementService>();
            services.AddScoped<IContactService, ContactService>();

            #endregion

            #region Repositories DI
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ISettlementRepository, SettlementRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            #endregion

            #region Autommaper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion


            var serviceProvider = services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionMiddleware();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address Contact API");
            });
        }
    }
}
