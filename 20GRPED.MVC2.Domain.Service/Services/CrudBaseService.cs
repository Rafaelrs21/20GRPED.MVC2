using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public abstract class CrudBaseService<TEntity> : ICrudBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ICrudBaseRepository<TEntity> CrudBaseRepository;

        protected CrudBaseService(
            ICrudBaseRepository<TEntity> crudBaseRepository)
        {
            CrudBaseRepository = crudBaseRepository;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await CrudBaseRepository.GetAllAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await CrudBaseRepository.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(TEntity insertedEntity)
        {
            await CrudBaseRepository.InsertAsync(insertedEntity);
        }

        public virtual async Task UpdateAsync(TEntity updatedEntity)
        {
            await CrudBaseRepository.UpdateAsync(updatedEntity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await CrudBaseRepository.DeleteAsync(id);
        }
    }
}
