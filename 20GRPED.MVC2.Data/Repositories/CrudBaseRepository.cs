using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Data.Context;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace _20GRPED.MVC2.Data.Repositories
{
    public abstract class CrudBaseRepository<TEntity> : ICrudBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BibliotecaContext BibliotecaContext;
        protected readonly DbSet<TEntity> DbSet;

        protected CrudBaseRepository(
            BibliotecaContext bibliotecaContext)
        {
            BibliotecaContext = bibliotecaContext;
            DbSet = bibliotecaContext.Set<TEntity>();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var autorModel = await DbSet.FindAsync(id);
            DbSet.Remove(autorModel);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task InsertAsync(TEntity insertedEntity)
        {
            await BibliotecaContext.AddAsync(insertedEntity);
        }

        public virtual async Task UpdateAsync(TEntity updatedEntity)
        {
            try
            {
                BibliotecaContext.Update(updatedEntity);
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
