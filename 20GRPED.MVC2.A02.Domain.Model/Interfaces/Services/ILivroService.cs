using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.A02.Domain.Model.Entities;

namespace _20GRPED.MVC2.A02.Domain.Model.Interfaces.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroEntity>> GetAllAsync();
        Task<LivroEntity> GetByIdAsync(int id);
        Task InsertAsync(LivroEntity updatedEntity);
        Task UpdateAsync(LivroEntity insertedEntity);
        Task DeleteAsync(int id);
    }
}
