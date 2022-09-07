using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestDevPace.Business.Infrastructure.Mapper;
using TestDevPace.Business.Interfaces;
using TestDevPace.Business.Services;
using TestDevPace.Data;
using TestDevPace.Data.EF;
using TestDevPace.Data.Repositories;

namespace TestDevPace.Business.DI
{
    public static class ModuleService
    {
        public static void AddDataLayer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TestDevPaceContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddBusinessLayer(this IServiceCollection services) 
        {
            services.AddSingleton<IMapper>(new Mapper(new MapperConfiguration(cfg =>
                cfg.AddProfile<MapperProfile>())));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
