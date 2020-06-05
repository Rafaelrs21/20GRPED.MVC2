using System;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Exceptions;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorService _autorService;
        private readonly IUnitOfWork _unitOfWork;

        public LivroService(
            ILivroRepository livroRepository,
            IAutorService autorService,
            IUnitOfWork unitOfWork)
        {
            _livroRepository = livroRepository;
            _autorService = autorService;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.BeginTransaction();
            await _livroRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
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

            _unitOfWork.BeginTransaction();

            if (!(livroAutorAggregateEntity.AutorEntity is null) &&
                !string.IsNullOrWhiteSpace(livroAutorAggregateEntity.AutorEntity.Nome) &&
                !string.IsNullOrWhiteSpace(livroAutorAggregateEntity.AutorEntity.UltimoNome))
            {
                await _autorService.InsertAsync(livroAutorAggregateEntity.AutorEntity);
            }

            livroAutorAggregateEntity.LivroEntity.Autor = livroAutorAggregateEntity.AutorEntity;
            await _livroRepository.InsertAsync(livroAutorAggregateEntity.LivroEntity);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(LivroEntity updatedEntity)
        {
            var isbnExists = await _livroRepository.CheckIsbnAsync(updatedEntity.Isbn, updatedEntity.Id);
            if (isbnExists)
            {
                throw new EntityValidationException(nameof(LivroEntity.Isbn), $"ISBN {updatedEntity.Isbn} já existe em outro livro!");
            }

            _unitOfWork.BeginTransaction();
            await _livroRepository.UpdateAsync(updatedEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> CheckIsbnAsync(string isbn, int id)
        {
            return await _livroRepository.CheckIsbnAsync(isbn, id);
        }
    }
}
