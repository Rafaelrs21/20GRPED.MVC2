using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Data.Repositories;
using _20GRPED.MVC2.Data.UoW;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;
using _20GRPED.MVC2.Domain.Model.Options;
using _20GRPED.MVC2.Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _20GRPED.MVC2.InversionOfControl
{
    public static class DependencyInjection
    {
        public static void RegisterInjections(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<BibliotecaContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("BibliotecaContext")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IAutorRepository, AutorRepository>();
        }
    }
}
