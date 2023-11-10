using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.IoC
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddDatabase();
            services.AddRepositories();
            services.AddServices();
            services.AddDTOMapper();
        }

        private static void AddDatabase([NotNull] this IServiceCollection services)
        {
            services.AddScoped(_ => new AppDbContext());
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void AddRepositories([NotNull] this IServiceCollection services)
        {
            services.AddScoped<ITb01Repository, Tb01Repository>();
        }

        private static void AddServices([NotNull] this IServiceCollection services)
        {
            services.AddScoped<ITb01Service, Tb01Service>();
        }

        private static void AddDTOMapper([NotNull] this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
        }
    }
}
