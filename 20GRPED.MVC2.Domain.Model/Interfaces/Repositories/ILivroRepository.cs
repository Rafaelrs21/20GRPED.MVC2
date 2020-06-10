using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Repositories
{
    public interface ILivroRepository : ICrudBaseRepository<LivroEntity>
    {
        Task<bool> CheckIsbnAsync(string isbn, int id = -1);
    }
}
