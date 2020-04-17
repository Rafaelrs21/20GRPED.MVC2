using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.A02.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.A02.Domain.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<LivroModel>> GetAllAsync()
        {
            return await _livroRepository.GetAllAsync();
        }

        public async Task<LivroModel> GetByIdAsync(int id)
        {
            return await _livroRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(LivroModel updatedModel)
        {
            await _livroRepository.InsertAsync(updatedModel);
        }

        public async Task UpdateAsync(LivroModel insertedModel)
        {
            await _livroRepository.UpdateAsync(insertedModel);
        }
    }
}
