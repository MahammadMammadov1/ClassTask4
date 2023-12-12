using Mamba.Data.DAL;
using Mamba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace WebApplication3.Controllers
{
	public class HomeController : Controller
	{
        private readonly AppDbContext _appDb;

        public HomeController(AppDbContext appDb)
        {
            this._appDb = appDb;
        }
        public IActionResult Index()
		{
			HomeViewModel homeViewModel = new HomeViewModel();
			homeViewModel.Teams = _appDb.Teams.Include(a=>a.Profession).ToList();	
			homeViewModel.Professions = _appDb.Professions.ToList();	

			return View(homeViewModel);
		}

		

		
	}
}