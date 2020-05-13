using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
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
            //services.Configure<TestOption>(configuration.GetSection("TestOption"));
            services.AddOptions<TestOption>()
                .Configure(option =>
                {
                    option.ExampleString = configuration.GetValue<string>("TestOption:ExampleString");
                    option.ExampleBool = configuration.GetValue<bool>("TestOption:ExampleBool");
                    option.ExampleInt = configuration.GetValue<int>("TestOption:ExampleInt");
                })
                .Validate(x=> x.Validate(), "Validação de ExampleString falhou");
        }
    }
}
