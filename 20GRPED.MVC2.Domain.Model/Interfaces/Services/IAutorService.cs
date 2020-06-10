using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Services
{
    public interface IAutorService : ICrudBaseService<AutorEntity>, IAutorRepository
    {
    }
}
