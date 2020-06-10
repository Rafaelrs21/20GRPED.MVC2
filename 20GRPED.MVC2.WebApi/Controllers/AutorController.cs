using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        private readonly IUnitOfWork _unitOfWork;

        public AutorController(
            IAutorService autorService,
            IUnitOfWork unitOfWork)
        {
            _autorService = autorService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutorEntity()
        {
            var autors = await _autorService.GetAllAsync();
            return Ok(autors.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorEntity>> GetAutorEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var autorEntity = await _autorService.GetByIdAsync(id);

            if (autorEntity == null)
            {
                return NotFound("Not found message test!");
            }

            return autorEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutorEntity(int id, AutorEntity autorEntity)
        {
            if (id != autorEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.BeginTransaction();
                await _autorService.UpdateAsync(autorEntity);
                await _unitOfWork.CommitAsync();
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<AutorEntity>> PostAutorEntity(AutorEntity autorEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _unitOfWork.BeginTransaction();
            await _autorService.InsertAsync(autorEntity);
            await _unitOfWork.CommitAsync();

            return Ok(autorEntity);
        }

        // DELETE: api/Autor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AutorEntity>> DeleteAutorEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var autorEntity = await _autorService.GetByIdAsync(id);
            if (autorEntity == null)
            {
                return NotFound();
            }

            _unitOfWork.BeginTransaction();
            await _autorService.DeleteAsync(id);
            await _unitOfWork.CommitAsync();

            return Ok(autorEntity);
        }
    }
}
