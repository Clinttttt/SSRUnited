using SSRUnited.Client.Interface;
using SSRUnited.Client.Services;

namespace SSRUnited.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection SharedClient(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient<ICrudApiClient, CrudApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7286");
            });


            return services;
        }
    }
}
