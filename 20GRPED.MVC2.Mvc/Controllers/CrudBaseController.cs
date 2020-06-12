using System.Threading.Tasks;
using _20GRPED.MVC2.Application.AppServices;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    [Authorize]
    public abstract class CrudBaseController<TAppService, TEntity> : Controller
        where TEntity : BaseEntity
        where TAppService : ICrudBaseAppService<TEntity>
    {
        private readonly TAppService _appService;

        protected CrudBaseController(
            TAppService appService)
        {
            _appService = appService;
        }

        // GET: Autor
        public virtual async Task<IActionResult> Index()
        {
            var autores = await _appService.GetAllAsync();

            return View(autores);
        }

        // GET: Autor/Details/5
        public virtual async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _appService.GetByIdAsync(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            return View(autorModel);
        }

        // GET: Autor/Create
        public virtual async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            if (ModelState.IsValid)
            {
                await _appService.InsertAsync(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        // GET: Autor/Edit/5
        public virtual async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _appService.GetByIdAsync(id.Value);
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
        public virtual async Task<IActionResult> Edit(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _appService.UpdateAsync(entity);
                }
                catch (EntityValidationException e)
                {
                    ModelState.AddModelError(e.PropertyName, e.Message);
                    return View(entity);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _appService.GetByIdAsync(id) == null)
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
            return View(entity);
        }

        // GET: Autor/Delete/5
        public virtual async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autorModel = await _appService.GetByIdAsync(id.Value);
            if (autorModel == null)
            {
                return NotFound();
            }

            return View(autorModel);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
