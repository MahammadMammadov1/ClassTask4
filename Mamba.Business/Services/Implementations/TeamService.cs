using Mamba.Business.Exceptions;
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
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public async  Task CreateAsync(Team entity)
        {
            string fileName = "";
            if (entity.FormFile != null)
            {
                fileName = entity.FormFile.FileName;
                if (entity.FormFile.ContentType != "image/jpeg" && entity.FormFile.ContentType != "image/png")
                {
                    throw new TotalTeamExceptions("FormFile", "png or jpeg file");
                }

                if (entity.FormFile.Length > 1048576)
                {
                    throw new TotalTeamExceptions("FormFile", "file must be lower than 1 mb");
                }

                if (entity.FormFile.FileName.Length > 64)
                {
                    fileName = fileName.Substring(fileName.Length - 64, 64);
                }

                fileName = Guid.NewGuid().ToString() + fileName;

                string path = "C:\\Users\\II Novbe\\Desktop\\TasksCode\\WebApplication3\\WebApplication3\\wwwroot\\Uploads\\Team\\" + fileName;
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    entity.FormFile.CopyTo(fileStream);
                }
                entity.Image = fileName;
            }
            else
            {
                throw new TotalTeamExceptions("FormFile", "image is required");

            }

            await _teamRepository.CreateAsync(entity);
            await _teamRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var prof = await _teamRepository.GetByIdAsync(x => x.Id == id && x.IsDeleted == false);
            if (prof == null) throw new NullReferenceException();
            _teamRepository.DeleteAsync(prof);
            await _teamRepository.CommitAsync();
        }

        public Task<List<Team>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Team> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Team entity)
        {
            throw new NotImplementedException();
        }
    }
}
