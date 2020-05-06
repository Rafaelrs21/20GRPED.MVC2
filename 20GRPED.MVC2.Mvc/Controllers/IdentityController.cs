using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using _20GRPED.MVC2.Crosscutting.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    public class IdentityController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddAdminClaim(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddClaimAsync(user, new Claim("AdminClaim", "asd"));
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
            await _signInManager.RefreshSignInAsync(user);

            return RedirectToAction("Index", "Livro");
        }
    }
}