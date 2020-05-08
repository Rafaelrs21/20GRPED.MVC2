using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _20GRPED.MVC2.Crosscutting.Identity
{
    public static class IdentityRegistration
    {
        public static void RegisterIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<LoginContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("LoginContextConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<LoginContext>();
        }
    }
}
