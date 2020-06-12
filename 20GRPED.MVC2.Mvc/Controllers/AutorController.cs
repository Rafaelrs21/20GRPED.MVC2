using _20GRPED.MVC2.Application.AppServices;
using _20GRPED.MVC2.Domain.Model.Entities;
using Microsoft.AspNetCore.Authorization;

namespace _20GRPED.MVC2.Mvc.Controllers
{
    [Authorize]
    public class AutorController : CrudBaseController<IAutorAppService, AutorEntity>
    {
        private readonly IAutorAppService _autorAppService;

        public AutorController(
            IAutorAppService autorAppService) : base(autorAppService)
        {
            _autorAppService = autorAppService;
        }
    }
}
