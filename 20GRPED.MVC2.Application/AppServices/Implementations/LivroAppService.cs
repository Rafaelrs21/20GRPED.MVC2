using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;

namespace _20GRPED.MVC2.Application.AppServices.Implementations
{
    public class LivroAppService : CrudBaseAppService<ILivroService, LivroEntity>, ILivroAppService
    {
        private readonly ILivroService _livroService;

        public LivroAppService(
            ILivroService livroService, 
            IUnitOfWork unitOfWork) : base(livroService, unitOfWork)
        {
            _livroService = livroService;
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id = -1)
        {
            return await _livroService.CheckIsbnAsync(isbn, id);
        }

        public async Task InsertAsync(LivroAutorAggregateEntity livroAutorAggregateEntity)
        {
            UnitOfWork.BeginTransaction();
            await _livroService.InsertAsync(livroAutorAggregateEntity);
            await UnitOfWork.CommitAsync();
        }
    }
}
