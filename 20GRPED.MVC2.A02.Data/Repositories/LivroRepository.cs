using _20GRPED.MVC2.A02.Data.Context;
using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC2.A02.Domain.Model.Entities;

namespace _20GRPED.MVC2.A02.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaContext _context;

        public LivroRepository(
            BibliotecaContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var livroModel = await _context.Livros.FindAsync(id);
            _context.Livros.Remove(livroModel);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckIsbnAsync(string updatedEntityIsbn)
        {
            var isbnExists = await _context.Livros.AnyAsync(x => x.Isbn == updatedEntityIsbn);

            return isbnExists;
        }

        public async Task<IEnumerable<LivroEntity>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<LivroEntity> GetByIdAsync(int id)
        {
            return await _context.Livros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(LivroEntity updatedEntity)
        {
            _context.Add(updatedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LivroEntity insertedEntity)
        {
            _context.Update(insertedEntity);
            await _context.SaveChangesAsync();
        }
    }
}
