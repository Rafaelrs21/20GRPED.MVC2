using _20GRPED.MVC2.A01.Mvc.Areas.Identity;
using _20GRPED.MVC2.A02.Crosscutting.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace _20GRPED.MVC2.A01.Mvc.Areas.Identity
{
    //Esse arquivo deveria estar no Crosscutting.Identity tbm, mas não consegui resolver esta parte.
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<LoginContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LoginContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LoginContext>();

                services.AddRazorPages();
            });
        }
    }
}