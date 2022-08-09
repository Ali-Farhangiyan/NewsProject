using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Infrastructure.IdentityService
{
    public static class MyIdentityService
    {
        public static IServiceCollection AddMyIdentityService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<IdentityDatabaseContext>(op => op.UseSqlServer(configuration["ConnectionString:SqlServerConnection"]));
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<IdentityDatabaseContext>();
            services.AddTransient<IIdentityDatabaseContext, IdentityDatabaseContext>();

            return services;
        }
    }
}
