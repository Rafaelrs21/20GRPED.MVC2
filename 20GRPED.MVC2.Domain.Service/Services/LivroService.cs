using System;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorService _autorService;

        public LivroService(
            ILivroRepository livroRepository,
            IAutorService autorService)
        {
            _livroRepository = livroRepository;
            _autorService = autorService;
        }

        public async Task DeleteAsync(int id)
        {
            await _livroRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LivroEntity>> GetAllAsync()
        {
            return await _livroRepository.GetAllAsync();
        }

        public async Task<LivroEntity> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(LivroAutorAggregateEntity livroAutorAggregateEntity)
        {
            var isbnExists = await _livroRepository.CheckIsbnAsync(livroAutorAggregateEntity.LivroEntity.Isbn);
            if (isbnExists)
            {
                throw new EntityValidationException(nameof(LivroEntity.Isbn), $"ISBN {livroAutorAggregateEntity.LivroEntity.Isbn} já existe!");
            }

            if (livroAutorAggregateEntity.AutorEntity is null)
            {
                await _livroRepository.InsertAsync(livroAutorAggregateEntity.LivroEntity);
            }
            else
            {
                await _livroRepository.InsertAsync(livroAutorAggregateEntity.LivroEntity);
                await _autorService.InsertAsync(livroAutorAggregateEntity.AutorEntity);
            }
        }

        public async Task UpdateAsync(LivroEntity updatedEntity)
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
