using _20GRPED.MVC2.A02.Data.Context;
using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.A02.Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<LivroModel>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<LivroModel> GetByIdAsync(int id)
        {
            return await _context.Livros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(LivroModel updatedModel)
        {
            _context.Add(updatedModel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LivroModel insertedModel)
        {
            _context.Update(insertedModel);
            await _context.SaveChangesAsync();
        }
    }
}
