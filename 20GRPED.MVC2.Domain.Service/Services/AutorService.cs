using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class AutorService : CrudBaseService<AutorEntity>, IAutorService
    {
        public AutorService(
            IAutorRepository autorRepository) : base(autorRepository)
        {
        }
    }
}
