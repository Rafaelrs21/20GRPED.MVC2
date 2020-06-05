using System.Collections.Generic;
using System.Threading.Tasks;
using _20GRPED.MVC2.Domain.Model.Entities;
using _20GRPED.MVC2.Domain.Model.Interfaces.Repositories;
using _20GRPED.MVC2.Domain.Model.Interfaces.Services;
using _20GRPED.MVC2.Domain.Model.Interfaces.UoW;

namespace _20GRPED.MVC2.Domain.Service.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AutorService(
            IAutorRepository autorRepository,
            IUnitOfWork unitOfWork)
        {
            _autorRepository = autorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.BeginTransaction();
            await _autorRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
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
            _unitOfWork.BeginTransaction();
            await _autorRepository.InsertAsync(insertedEntity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(AutorEntity updatedEntity)
        {
            _unitOfWork.BeginTransaction();
            await _autorRepository.UpdateAsync(updatedEntity);
            await _unitOfWork.CommitAsync();
        }
    }
}
