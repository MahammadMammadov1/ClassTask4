using Mamba.Business.Exceptions;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamService teamService,ITeamRepository teamRepository)
        {
            _teamService = teamService;
            this._teamRepository = teamRepository;
        }
        public async Task<IActionResult> Index()
        {
            List<Team> prof = _teamRepository.Table.ToList();
            return View(prof);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _teamService.CreateAsync(team);
            }
            catch (TotalTeamExceptions ex)
            {
                ModelState.AddModelError(ex.Msg, ex.Message);
                return View();
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var wanted = await _teamService.GetByIdAsync(id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _teamService.UpdateAsync(team);
            }
            catch (Exception)
            {

            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _teamService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
