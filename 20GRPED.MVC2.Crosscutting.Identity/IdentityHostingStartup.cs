//using _20GRPED.MVC2.Crosscutting.Identity;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(IdentityHostingStartup))]
//namespace _20GRPED.MVC2.Crosscutting.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) => {
//                services.AddDbContext<LoginContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("LoginContextConnection")));

//                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                //    .AddEntityFrameworkStores<LoginContext>();

//                //services.AddIdentityCore<IdentityUser>()
//                //    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>()
//                //    .AddEntityFrameworkStores<IdentityDbContext>()
//                //    .AddDefaultTokenProviders()
//                //    .AddDefaultUI();
//            });
//        }
//    }
//}