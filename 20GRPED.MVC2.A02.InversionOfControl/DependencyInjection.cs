using _20GRPED.MVC2.A02.Data.Context;
using _20GRPED.MVC2.A02.Data.Repositories;
using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.A02.Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _20GRPED.MVC2.A02.InversionOfControl
{
    public static class DependencyInjection
    {
        public static void Register(
            IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BibliotecaContext")));

            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ILivroRepository, LivroRepository>();
        }
    }
}
