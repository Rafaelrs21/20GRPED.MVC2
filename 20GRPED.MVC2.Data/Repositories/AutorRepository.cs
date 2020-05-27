using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Data.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly BibliotecaContext _context;

        public AutorRepository(
            BibliotecaContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id)
        {
            var autorModel = await _context.Autores.FindAsync(id);
            _context.Autores.Remove(autorModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AutorEntity>> GetAllAsync()
        {
            return await _context.Autores.ToListAsync();
        }

        public async Task<AutorEntity> GetByIdAsync(int id)
        {
            return await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertAsync(AutorEntity insertedEntity)
        {
            _context.Add(insertedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutorEntity updatedEntity)
        {
            try
            {
                _context.Update(updatedEntity);
                await _context.SaveChangesAsync();
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
