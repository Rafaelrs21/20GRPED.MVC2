using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;

namespace _20GRPED.MVC2.Application.AppServices.Implementations
{
    public abstract class CrudBaseAppService<TDomainService, TEntity> : ICrudBaseAppService<TEntity>
        where TEntity : BaseEntity
        where TDomainService : ICrudBaseService<TEntity>
    {
        protected readonly TDomainService DomainService;
        protected readonly IUnitOfWork UnitOfWork;

        protected CrudBaseAppService(
            TDomainService domainService,
            IUnitOfWork unitOfWork)
        {
            DomainService = domainService;
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DomainService.GetAllAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DomainService.GetByIdAsync(id);
        }

        public virtual async Task InsertAsync(TEntity insertedEntity)
        {
            UnitOfWork.BeginTransaction();
            await DomainService.InsertAsync(insertedEntity);
            await UnitOfWork.CommitAsync();
        }

        public virtual async Task UpdateAsync(TEntity updatedEntity)
        {
            UnitOfWork.BeginTransaction();
            await DomainService.UpdateAsync(updatedEntity);
            await UnitOfWork.CommitAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            UnitOfWork.BeginTransaction();
            await DomainService.DeleteAsync(id);
            await UnitOfWork.CommitAsync();
        }
    }
}
