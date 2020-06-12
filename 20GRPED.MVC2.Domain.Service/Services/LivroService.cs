using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class LivroService : CrudBaseService<LivroEntity>, ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorService _autorService;

        public LivroService(
            ILivroRepository livroRepository,
            IAutorService autorService) : base(livroRepository)
        {
            _livroRepository = livroRepository;
            _autorService = autorService;
        }

        public async Task InsertAsync(LivroAutorAggregateEntity livroAutorAggregateEntity)
        {
            var isbnExists = await _livroRepository.CheckIsbnAsync(livroAutorAggregateEntity.LivroEntity.Isbn);
            if (isbnExists)
            {
                throw new EntityValidationException(nameof(LivroEntity.Isbn), $"ISBN {livroAutorAggregateEntity.LivroEntity.Isbn} já existe!");
            }

            if (!(livroAutorAggregateEntity.AutorEntity is null) &&
                !string.IsNullOrWhiteSpace(livroAutorAggregateEntity.AutorEntity.Nome) &&
                !string.IsNullOrWhiteSpace(livroAutorAggregateEntity.AutorEntity.UltimoNome))
            {
                await _autorService.InsertAsync(livroAutorAggregateEntity.AutorEntity);
            }

            livroAutorAggregateEntity.LivroEntity.Autor = livroAutorAggregateEntity.AutorEntity;
            await InsertAsync(livroAutorAggregateEntity.LivroEntity);
        }

        public override async Task InsertAsync(LivroEntity insertedEntity)
        {
            var isbnExists = await _livroRepository.CheckIsbnAsync(insertedEntity.Isbn);
            if (isbnExists)
            {
                throw new EntityValidationException(nameof(LivroEntity.Isbn), $"ISBN {insertedEntity.Isbn} já existe!");
            }
            await base.InsertAsync(insertedEntity);
        }

        public override async Task UpdateAsync(LivroEntity updatedEntity)
        {
            var isbnExists = await _livroRepository.CheckIsbnAsync(updatedEntity.Isbn, updatedEntity.Id);
            if (isbnExists)
            {
                throw new EntityValidationException(nameof(LivroEntity.Isbn), $"ISBN {updatedEntity.Isbn} já existe em outro livro!");
            }

            await _livroRepository.UpdateAsync(updatedEntity);
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id)
        {
            return await _livroRepository.CheckIsbnAsync(isbn, id);
        }
    }
}
