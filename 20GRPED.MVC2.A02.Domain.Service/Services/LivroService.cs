﻿using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.A02.Domain.Model.Entities;

namespace _20GRPED.MVC2.A02.Domain.Service.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(
            ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
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

        public async Task InsertAsync(LivroEntity updatedEntity)
        {
            await _livroRepository.InsertAsync(updatedEntity);
        }

        public async Task UpdateAsync(LivroEntity insertedEntity)
        {
            await _livroRepository.UpdateAsync(insertedEntity);
        }
    }
}