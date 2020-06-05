using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Services
{
    public interface ILivroService : ICrudBaseService<LivroEntity>
    {
        Task<bool> CheckIsbnAsync(string isbn, int id);
    }
}
