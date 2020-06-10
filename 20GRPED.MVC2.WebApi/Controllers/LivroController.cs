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
    public class LivroController : CrudBaseController<ILivroService, LivroEntity>
    {
        private readonly ILivroService _livroService;

        public LivroController(
            ILivroService livroService,
            IUnitOfWork unitOfWork) : base(livroService, unitOfWork)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public async Task<ActionResult<LivroEntity>> Post(LivroAutorAggregateEntity livroAutorAggregateEntity)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                UnitOfWork.BeginTransaction();
                await _livroService.InsertAsync(livroAutorAggregateEntity);
                await UnitOfWork.CommitAsync();

                return Ok(livroAutorAggregateEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpGet("CheckIsbn/{isbn}/{id}")]
        public async Task<ActionResult<bool>> CheckIsbnAsync(string isbn, int id)
        {
            var isIsbnValid = await _livroService.CheckIsbnAsync(isbn, id);

            return Ok(isIsbnValid);
        }
    }
}
