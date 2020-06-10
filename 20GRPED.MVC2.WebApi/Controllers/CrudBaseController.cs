using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CrudBaseController<TDomainService, TEntity> : ControllerBase
        where TEntity : BaseEntity
        where TDomainService : ICrudBaseService<TEntity>
    {
        private readonly TDomainService _domainService;
        protected readonly IUnitOfWork UnitOfWork;

        protected CrudBaseController(
            TDomainService domainService,
            IUnitOfWork unitOfWork)
        {
            _domainService = domainService;
            UnitOfWork = unitOfWork;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            var livros = await _domainService.GetAllAsync();
            return Ok(livros.ToList());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _domainService.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, TEntity entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            try
            {
                UnitOfWork.BeginTransaction();
                await _domainService.UpdateAsync(entity);
                await UnitOfWork.CommitAsync();

                return Ok();
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                UnitOfWork.BeginTransaction();
                await _domainService.InsertAsync(entity);
                await UnitOfWork.CommitAsync();

                return Ok(entity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TEntity>> Delete(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var entity = await _domainService.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            UnitOfWork.BeginTransaction();
            await _domainService.DeleteAsync(id);
            await UnitOfWork.CommitAsync();

            return Ok(entity);
        }
    }
}
