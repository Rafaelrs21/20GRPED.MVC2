using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutorController : CrudBaseController<IAutorService, AutorEntity>
    {
        public AutorController(
            IAutorService autorService,
            IUnitOfWork unitOfWork) : base(autorService, unitOfWork)
        {
        }
    }
}
