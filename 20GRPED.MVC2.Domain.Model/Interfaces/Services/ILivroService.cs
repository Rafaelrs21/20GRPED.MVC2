using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Services
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroEntity>> GetAllAsync();
        Task<LivroEntity> GetByIdAsync(int id);
        Task InsertAsync(LivroAutorAggregateEntity livroAutorAggregateEntity);
        Task UpdateAsync(LivroEntity updatedEntity);
        Task DeleteAsync(int id);
        Task<bool> CheckIsbnAsync(string isbn, int id);
    }
}
