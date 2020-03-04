using Contracts;
using Entities.Data;
using LoggerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using CompanyEmployees.Custom;
//using Repository;
namespace CompanyEmployees.Extenstions
{
    public static class ServicesExtenstion
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(option =>
            {
                option.AddPolicy("CorsPloicy", builder =>
               {
                   builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
               });
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureLoggerServices(this IServiceCollection services) =>
            services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositryContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                builder => builder.MigrationsAssembly("CompanyEmployees")));

        public static void ConfigureRepositroryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositroryManager, RepositroryManager>();
        }

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsVOutputFormatter()));
    }
}
