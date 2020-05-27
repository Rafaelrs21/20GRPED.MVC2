using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(
            IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task DeleteAsync(int id)
        {
            await _autorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AutorEntity>> GetAllAsync()
        {
            return await _autorRepository.GetAllAsync();
        }

        public async Task<AutorEntity> GetByIdAsync(int id)
        {
            return await _autorRepository.GetByIdAsync(id);
        }

        public async Task InsertAsync(AutorEntity insertedEntity)
        {
            await _autorRepository.InsertAsync(insertedEntity);
        }

        public async Task UpdateAsync(AutorEntity updatedEntity)
        {
            await _autorRepository.UpdateAsync(updatedEntity);
        }
    }
}
