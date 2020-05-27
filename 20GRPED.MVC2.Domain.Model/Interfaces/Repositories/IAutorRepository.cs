using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Repositories
{
    public interface IAutorRepository
    {
        Task<IEnumerable<AutorEntity>> GetAllAsync();
        Task<AutorEntity> GetByIdAsync(int id);
        Task InsertAsync(AutorEntity insertedEntity);
        Task UpdateAsync(AutorEntity updatedEntity);
        Task DeleteAsync(int id);
    }
}
