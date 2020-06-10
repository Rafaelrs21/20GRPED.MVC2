using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;

namespace _20GRPED.MVC2.Application.AppServices
{
    public interface ICrudBaseAppService<TEntity> : ICrudBaseService<TEntity>
        where TEntity : BaseEntity
    {
    }
}
