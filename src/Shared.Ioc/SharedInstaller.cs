using AutoMapper;
using DataAccess.Contracts;
using DataAccess.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace Shared.Ioc
{
    public static class SharedInstaller
    {
        public static void RegisterServices(IServiceCollection services)
        {
            var appSettings = services.BuildServiceProvider().GetService<IOptions<Settings>>().Value;
            services.AddTransient<IPatientsRepository, PatientsRepository>();
            services.AddDbContext<PatientsDbContext>(options =>
                options.UseSqlServer(appSettings.DataAccessSettings.ConnectionString));
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper());

        }
    }
}
