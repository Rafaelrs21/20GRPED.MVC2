using System.Threading.Tasks;

namespace _20GRPED.MVC2.Domain.Model.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task CommitAsync();
    }
}
