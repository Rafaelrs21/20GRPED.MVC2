using System;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Options;
using Microsoft.Extensions.Options;

namespace _20GRPED.MVC2.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly BibliotecaContext _context;
        private readonly IOptionsMonitor<TestOption> _testOption;

        public LivroRepository(
            BibliotecaContext context,
            IOptionsMonitor<TestOption> testOption)
        {
            _context = context;
            _testOption = testOption;
        }

        public async Task DeleteAsync(int id)
        {
            var livroModel = await _context.Livros.FindAsync(id);
            _context.Livros.Remove(livroModel);
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id = 0)
        {
            var isbnExists = await _context.Livros.AnyAsync(x => x.Isbn == isbn && x.Id != id);

            return isbnExists;
        }

        public async Task<LivroEntity> GetByIsbnAsync(string isbn)
        {
            return await _context.Livros.SingleOrDefaultAsync(x => x.Isbn == isbn);
        }

        public async Task<IEnumerable<LivroEntity>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<LivroEntity> GetByIdAsync(int id)
        {
            return await _context.Livros.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(LivroEntity insertedEntity)
        {
            await _context.AddAsync(insertedEntity);

            //usado para testar o transaction
            //await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LivroEntity updatedEntity)
        {
            try
            {
                _context.Update(updatedEntity);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await GetByIdAsync(updatedEntity.Id) == null)
                {
                    throw new RepositoryException("Livro não encontrado!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
