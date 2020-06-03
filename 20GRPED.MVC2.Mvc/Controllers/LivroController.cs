using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Mvc.HttpServices;
using _20GRPED.MVC2.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;
        private readonly IAutorHttpService _autorService;

        public LivroController(
            ILivroService livroService,
            IAutorHttpService autorService)
        {
            _livroService = livroService;
            _autorService = autorService;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            var livros = await _livroService.GetAllAsync();

            if(livros == null)
                return Redirect("/Identity/Account/Login");

            return View(livros);
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroService.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            return View(livroModel);
        }

        // GET: Livro/Create
        public async Task<IActionResult> Create()
        {
            var livroViewModel = new LivroAutorAggregateViewModel(await _autorService.GetAllAsync());

            return View(livroViewModel);
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroAutorAggregateViewModel livroAutorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var livroAutorAggregateEntity = livroAutorViewModel.ToAggregateEntity();

                    await _livroService.InsertAsync(livroAutorAggregateEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(livroAutorViewModel);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroService.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            var livroViewModel = new LivroViewModel(livroModel, await _autorService.GetAllAsync());

            return View(livroViewModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroEntity livroEntity)
        {
            if (id != livroEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.UpdateAsync(livroEntity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(new LivroViewModel(livroEntity));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _livroService.GetByIdAsync(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new LivroViewModel(livroEntity));
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroService.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            return View(livroModel);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _livroService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckIsbn(string isbn, int id)
        {
            if (await _livroService.CheckIsbnAsync(isbn, id))
            {
                return Json($"ISBN {isbn} já existe!");
            }

            return Json(true);
        }
    }
}
