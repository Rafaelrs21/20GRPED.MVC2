using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroEntity>> GetAllAsync();
        Task<LivroEntity> GetByIdAsync(int id);
        Task InsertAsync(LivroEntity insertedEntity);
        Task UpdateAsync(LivroEntity updatedEntity);
        Task DeleteAsync(int id);
        Task<bool> CheckIsbnAsync(string isbn, int id = -1);
        Task<LivroEntity> GetByIsbnAsync(string isbn);
    }
}
