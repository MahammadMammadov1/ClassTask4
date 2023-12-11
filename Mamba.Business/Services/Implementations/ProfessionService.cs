using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Implementations
{
    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository _professionRepository;

        public ProfessionService(IProfessionRepository professionRepository)
        {
           _professionRepository = professionRepository;
            
        }
        public async Task CreateAsync(Profession entity)
        {
            if (!_professionRepository.Table.Any(x => x.Name == entity.Name))
            {
                await _professionRepository.CreateAsync(entity);
                await _professionRepository.CommitAsync();  
                
            }
            else
            {
                throw new Exception();
            }
            
        }

        public async Task Delete(int id)
        {
            var prof = await _professionRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (prof == null) throw new NullReferenceException();
            _professionRepository.DeleteAsync(prof);
            await _professionRepository.CommitAsync();
        }

        public async Task<List<Profession>> GetAllAsync()
        {
            return await _professionRepository.GetAllAsync();
        }

        public async Task<Profession> GetByIdAsync(int id)
        {
            var prof = await _professionRepository.GetByIdAsync(x => x.Id == id);
            return prof;
        }

        public async  Task UpdateAsync(Profession entity)
        {
            var prof = await _professionRepository.GetByIdAsync(x => x.Id == entity.Id && x.IsDeleted == false);
            if (prof == null) throw new NullReferenceException();

            if (_professionRepository.Table.Any(x => x.Name == entity.Name && prof.Id != entity.Id))
                throw new NullReferenceException();

            prof.Name = entity.Name;
            await _professionRepository.CommitAsync();
        }


    }
}
