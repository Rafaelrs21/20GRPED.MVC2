using _20GRPED.MVC2.Crosscutting.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityHostingStartup = _20GRPED.MVC2.Mvc.Areas.Identity.IdentityHostingStartup;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace _20GRPED.MVC2.Mvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                //Idealmente o AddDbContext deveria estar no Crosscutting, mas não consegui resolver esta parte.
                //Neste caso, este arquivo poderia ser removido
                services.AddDbContext<LoginContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LoginContextConnection")));

                //Idealmente o Add do Identity deveria estar no Crosscutting, mas não consegui resolver esta parte.
                //Neste caso, este arquivo poderia ser removido
                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LoginContext>();
            });
        }
    }
}