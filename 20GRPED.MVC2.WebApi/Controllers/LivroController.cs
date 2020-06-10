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
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        private readonly IUnitOfWork _unitOfWork;

        public LivroController(
            ILivroService livroService,
            IUnitOfWork unitOfWork)
        {
            _livroService = livroService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroEntity>>> GetLivroEntity()
        {
            var livros = await _livroService.GetAllAsync();
            return Ok(livros.ToList());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroEntity>> GetLivroEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var livroEntity = await _livroService.GetByIdAsync(id);

            if (livroEntity == null)
            {
                return NotFound();
            }

            return Ok(livroEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivroEntity(int id, LivroEntity livroEntity)
        {
            if (id != livroEntity.Id)
            {
                return BadRequest();
            }

            try
            {
                _unitOfWork.BeginTransaction();
                await _livroService.UpdateAsync(livroEntity);
                await _unitOfWork.CommitAsync();
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
        public async Task<ActionResult<LivroEntity>> PostLivroEntity(LivroAutorAggregateEntity livroAutorAggregateEntity)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                _unitOfWork.BeginTransaction();

                //Exemplo de transaction global do sistema - precisa do AsyncFlowOption se estivermos usando async!
                //using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

                await _livroService.InsertAsync(livroAutorAggregateEntity);

                //Esse Complete() seria o Commit, o rollback é se sair do escopo sem chamar o Complete()
                //transactionScope.Complete();
                await _unitOfWork.CommitAsync();

                return Ok(livroAutorAggregateEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LivroEntity>> DeleteLivroEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var livroEntity = await _livroService.GetByIdAsync(id);
            if (livroEntity == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            await _livroService.DeleteAsync(id); 
            await _unitOfWork.CommitAsync();

            return Ok(livroEntity);
        }

        [HttpGet("CheckIsbn/{isbn}/{id}")]
        public async Task<ActionResult<bool>> CheckIsbnAsync(string isbn, int id)
        {
            var isIsbnValid = await _livroService.CheckIsbnAsync(isbn, id);

            return Ok(isIsbnValid);
        }
    }
}
