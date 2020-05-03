//using _20GRPED.MVC2.Crosscutting.Identity;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;

//[assembly: HostingStartup(typeof(IdentityHostingStartup))]
//namespace _20GRPED.MVC2.Crosscutting.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                services.AddDbContext<LoginContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("LoginContextConnection")));


//                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                //    .AddEntityFrameworkStores<LoginContext>();

//                //services.AddRazorPages();

//                //services.AddIdentityCore<IdentityUser>(options =>
//                //        options.SignIn.RequireConfirmedAccount = true)
//                //    .AddEntityFrameworkStores<LoginContext>();

//                //services.TryAddScoped<IUserValidator<IdentityUser>, UserValidator<IdentityUser>>();
//                //services.TryAddScoped<IPasswordValidator<IdentityUser>, PasswordValidator<IdentityUser>>();
//                //services.TryAddScoped<IPasswordHasher<IdentityUser>, PasswordHasher<IdentityUser>>();
//                //services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
//                //services.TryAddScoped<IRoleValidator<IdentityRole>, RoleValidator<IdentityRole>>();

//                // No interface for the error describer so we can add errors without rev'ing the interface

//                //services.TryAddScoped<IdentityErrorDescriber>();
//                //services.TryAddScoped<IUserClaimsPrincipalFactory<IdentityUser>, UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>();
//                //services.TryAddScoped<IUserConfirmation<IdentityUser>, DefaultUserConfirmation<IdentityUser>>();

//                //services.TryAddScoped<UserManager<IdentityUser>>();

//                //services.TryAddScoped<SignInManager<IdentityUser>>();
//                //services.TryAddScoped<RoleManager<IdentityRole>>();
//            });
//        }
//    }
//}