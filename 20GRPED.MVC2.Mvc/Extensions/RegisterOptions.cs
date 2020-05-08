using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _20GRPED.MVC2.Mvc.Extensions
{
    public static class RegisterOptions
    {
        public static void RegisterConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<TestOption>(configuration.GetSection("TestOption"));
        }
    }
}
