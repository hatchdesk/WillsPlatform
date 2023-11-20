using Microsoft.Extensions.DependencyInjection;

namespace WillsPlatform.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile)); 
            return services;
        }
    }
}
