using Mamba.Business.Exceptions;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Mamba.Data.DAL;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProfessionRepository _professionRepository;
        private readonly AppDbContext _appDb;

        public TeamService(ITeamRepository teamRepository,IProfessionRepository professionRepository,AppDbContext appDb)
        {
            _teamRepository = teamRepository;
            _professionRepository = professionRepository;
            _appDb = appDb;
        }
        public async  Task CreateAsync(Team entity)
        {
            if (!_professionRepository.Table.Any(x => x.Id == entity.ProfessionId))
            {
                throw new TotalTeamExceptions("ProfessionId", "Profession not found!!!");
            }
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

                string path = "C:\\Users\\Mehemmed\\Desktop\\ClassTask4\\WebApplication3\\wwwroot\\Uploads\\Team\\" + fileName;
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

        public async Task<List<Team>> GetAllAsync()
        {
            return await _teamRepository.GetAllAsync(x=>x.IsDeleted == false,"Profession");
        }

        public Task<Team> GetByIdAsync(int id)
        {
            return _teamRepository.GetByIdAsync(x=>x.Id == id,"Profession");
        }

        public async Task UpdateAsync(Team entity)
        {
            var wanted = await _teamRepository.GetByIdAsync(x => x.Id == entity.Id,"Profession");

            if (wanted == null)
            {
                throw new NullReferenceException();
            }
            string oldFilePath = "C:\\Users\\II Novbe\\Desktop\\Pustok-Last-version\\Pustok\\wwwroot\\uploads\\sliders\\" + wanted.Image;

            if (entity.FormFile != null)
            {

                string newFileName = entity.FormFile.FileName;
                if (entity.FormFile.ContentType != "image/jpeg" && entity.FormFile.ContentType != "image/png")
                {
                    throw new TotalTeamExceptions("FormFile", "you can only add png or jpeg file");
                }

                if (entity.FormFile.Length > 1048576)
                {
                    throw new TotalTeamExceptions("FormFile", "file must be lower than 1 mb");
                }

                if (entity.FormFile.FileName.Length > 64)
                {
                    newFileName = newFileName.Substring(newFileName.Length - 64, 64);
                }

                newFileName = Guid.NewGuid().ToString() + newFileName;

                string newFilePath = "C:\\Users\\II Novbe\\Desktop\\Pustok-Last-version\\Pustok\\wwwroot\\uploads\\sliders\\" + newFileName;
                using (FileStream fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    entity.FormFile.CopyTo(fileStream);
                }

                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                wanted.Image = newFileName;
            }

            wanted.Fullname = entity.Fullname;
            wanted.FB = entity.FB;
            wanted.Image = entity.Image;
            wanted.Twit = entity.Twit;
            wanted.Linkedin = entity.Linkedin;
            wanted.Insta = entity.Linkedin;

            await _teamRepository.CommitAsync();
        }
    }
}
