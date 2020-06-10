using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;

namespace _20GRPED.MVC2.Application.AppServices.Implementations
{
    public class AutorAppService : CrudBaseAppService<IAutorService, AutorEntity>, IAutorAppService
    {
        public AutorAppService(
            IAutorService domainService, 
            IUnitOfWork unitOfWork) : base(domainService, unitOfWork)
        {
        }
    }
}
