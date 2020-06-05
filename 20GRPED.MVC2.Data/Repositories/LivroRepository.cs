using System.Threading.Tasks;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Data.Repositories
{
    public class LivroRepository : BaseRepository<LivroEntity>, ILivroRepository
    {
        public LivroRepository(
            BibliotecaContext bibliotecaContext) : base(bibliotecaContext)
        {
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id = 0)
        {
            var isbnExists = await DbSet.AnyAsync(x => x.Isbn == isbn && x.Id != id);

            return isbnExists;
        }

        public async Task<LivroEntity> GetByIsbnAsync(string isbn)
        {
            return await DbSet.SingleOrDefaultAsync(x => x.Isbn == isbn);
        }
    }
}
