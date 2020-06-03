using System.Net;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Mvc.HttpServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    [Authorize]
    public class AutorController : Controller
    {
        private readonly IAutorHttpService _autorService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AutorController(
            IAutorHttpService autorService,
            SignInManager<IdentityUser> signInManager)
        {
            _autorService = autorService;
            _signInManager = signInManager;
        }

        // GET: Autor
        public async Task<IActionResult> Index()
        {
            var autores = await _autorService.GetAllAsync();

            if (autores == null)
                return Redirect("/Identity/Account/Login");

            return View(autores);
        }

        // GET: Autor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpResponseMessage = await _autorService.GetByIdHttpAsync(id.Value);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    await _signInManager.SignOutAsync();
                    return Redirect("/Identity/Account/Login");
                }
                else
                {
                    var message = await httpResponseMessage.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, message);

                    var autores = await _autorService.GetAllAsync();
                    return View(nameof(Index), autores);
                }
            }

            var autorModel = JsonConvert.DeserializeObject<AutorEntity>(await httpResponseMessage.Content.ReadAsStringAsync());
            if (autorModel == null)
            {
                return NotFound();
            }

            return View(autorModel);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AutorEntity autorEntity)
        {
            if (ModelState.IsValid)
            {
                await _autorService.InsertAsync(autorEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(autorEntity);
        }

        // GET: Autor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _autorService.GetByIdAsync(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }
            return View(autorModel);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AutorEntity autorEntity)
        {
            if (id != autorEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _autorService.UpdateAsync(autorEntity);

                return RedirectToAction(nameof(Index));
            }
            return View(autorEntity);
        }

        // GET: Autor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _autorService.GetByIdAsync(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            return View(autorModel);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _autorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
