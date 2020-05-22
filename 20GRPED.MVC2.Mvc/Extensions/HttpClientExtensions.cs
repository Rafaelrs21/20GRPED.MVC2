using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Options;
using _20GRPED.MVC2.Mvc.HttpServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _20GRPED.MVC2.Mvc.Extensions
{
    public static class HttpClientExtensions
    {
        public static void RegisterHttpClients(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var bibliotecaHttpOptionsSection = configuration.GetSection(nameof(BibliotecaHttpOptions));
            var bibliotecaHttpOptions = bibliotecaHttpOptionsSection.Get<BibliotecaHttpOptions>();

            services.AddHttpClient(bibliotecaHttpOptions.Name, x => { x.BaseAddress = bibliotecaHttpOptions.ApiBaseUrl; });

            services.AddScoped<ILivroService, LivroHttpService>();
            services.AddScoped<IAuthHttpService, AuthHttpService>();
        }
    }
}
