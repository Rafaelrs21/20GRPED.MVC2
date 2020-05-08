using _20GRPED.MVC2.Crosscutting.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(_20GRPED.MVC2.Mvc.Areas.Identity.IdentityHostingStartup))]
namespace _20GRPED.MVC2.Mvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<LoginContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("LoginContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<LoginContext>();
            });
        }
    }
}