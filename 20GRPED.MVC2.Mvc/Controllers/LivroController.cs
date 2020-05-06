using System;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;

namespace _20GRPED.MVC1.A15.Mvc.Controllers
{
    public class LivroController : Controller
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            return View(await _livroService.GetAllAsync());
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Isbn,Lancamento,Paginas")] LivroEntity livroEntity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _livroService.InsertAsync(livroEntity);
                    return RedirectToAction(nameof(Index));
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                }
            }
            return View(livroEntity);
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
            return View(livroModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Isbn,Lancamento,Paginas")] LivroEntity livroEntity)
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
                    return View(livroEntity);
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
            return View(livroEntity);
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
