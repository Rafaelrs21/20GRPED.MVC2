using System.Threading.Tasks;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Data.Repositories
{
    public class AutorRepository : BaseRepository<AutorEntity>, IAutorRepository
    {
        public AutorRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
        }

        public override async Task<AutorEntity> GetByIdAsync(int id)
        {
            return await DbSet
                .Include(x => x.Livros)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
