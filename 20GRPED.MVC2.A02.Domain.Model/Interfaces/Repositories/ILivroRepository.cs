using _20GRPED.MVC2.A02.Domain.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC2.A02.Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroModel>> GetAllAsync();
        Task<LivroModel> GetByIdAsync(int id);
        Task InsertAsync(LivroModel updatedModel);
        Task UpdateAsync(LivroModel insertedModel);
        Task DeleteAsync(int id);
    }
}
