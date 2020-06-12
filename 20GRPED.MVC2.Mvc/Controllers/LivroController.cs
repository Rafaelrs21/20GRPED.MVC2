using System.Threading.Tasks;
using _20GRPED.MVC2.Application.AppServices;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    [Authorize]
    public class LivroController : CrudBaseController<ILivroAppService, LivroEntity>
    {
        private readonly ILivroAppService _livroAppService;
        private readonly IAutorAppService _autorAppService;

        public LivroController(
            ILivroAppService livroAppService,
            IAutorAppService autorAppService) : base(livroAppService)
        {
            _livroAppService = livroAppService;
            _autorAppService = autorAppService;
        }

        // GET: Livro/Create
        public override async Task<IActionResult> Create()
        {
            var livroViewModel = new LivroAutorAggregateViewModel(await _autorAppService.GetAllAsync());

            return View(livroViewModel);
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("CreateAggregate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroAutorAggregateViewModel livroAutorViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var livroAutorAggregateEntity = livroAutorViewModel.ToAggregateEntity();

                    await _livroAppService.InsertAsync(livroAutorAggregateEntity);
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
        public override async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroModel = await _livroAppService.GetByIdAsync(id.Value);
            if (livroModel == null)
            {
                return NotFound();
            }

            var livroViewModel = new LivroViewModel(livroModel, await _autorAppService.GetAllAsync());

            return View(livroViewModel);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Edit(int id, LivroEntity livroEntity)
        {
            if (id != livroEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _livroAppService.UpdateAsync(livroEntity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(new LivroViewModel(livroEntity));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _livroAppService.GetByIdAsync(id) == null)
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

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> CheckIsbn(string isbn, int id)
        {
            if (await _livroAppService.CheckIsbnAsync(isbn, id))
            {
                return Json($"ISBN {isbn} já existe!");
            }

            return Json(true);
        }
    }
}
