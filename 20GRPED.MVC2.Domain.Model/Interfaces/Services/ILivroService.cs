using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.Services
{
    public interface ILivroService : ICrudBaseService<LivroEntity>, ILivroRepository
    {
        Task InsertAsync(LivroAutorAggregateEntity livroAutorAggregateEntity);
    }
}
