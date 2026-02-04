namespace SSRUnited.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis"];
                options.InstanceName = "SSRUnited_";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Blazor", policy =>
                {
                    policy.WithOrigins("https://localhost:7201", "http://localhost:5019");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowCredentials();
                });
            });

            return services;
        }
    }
    
}

