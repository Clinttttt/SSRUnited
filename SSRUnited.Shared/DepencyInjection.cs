using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSRUnited.Shared.Data;
using SSRUnited.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSRUnited.Shared
{
    public static class DepencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddDbContext<ApplicatonDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            service.AddScoped<IIRespository, SSRUnited.Shared.Respository.Respository>();
            service.AddScoped<ApplicatonDbContext>();
            return service;
        }
    }
}
