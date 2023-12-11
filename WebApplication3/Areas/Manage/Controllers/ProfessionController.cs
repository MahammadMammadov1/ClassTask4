using Humanizer.Localisation;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Models;
using Mamba.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("Manage")]
    //[Authorize(Roles ="SuperAdmin,Admin")]
    public class ProfessionController : Controller
    {
        private readonly IProfessionService _professionService;
        private readonly IProfessionRepository _professionRepository;

        public ProfessionController(IProfessionService professionService,IProfessionRepository professionRepository )
        {
            this._professionService = professionService;
            this._professionRepository = professionRepository;
        }
        public IActionResult Index()
        {
            List<Profession> prof = _professionRepository.Table.ToList ();
            return View(prof);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Profession profession)
        {
            if(!ModelState.IsValid) return View();

            try
            {
                await _professionService.CreateAsync(profession);
            }
            catch (Exception)
            {
 
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var wanted = await _professionService.GetByIdAsync(id);
            if (wanted == null) return NotFound();
            return View(wanted);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Profession profession)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                await _professionService.UpdateAsync(profession);
            }
            catch (Exception)
            {

            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _professionService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
