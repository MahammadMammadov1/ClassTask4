using Mamba.Areas.Manage.ViewModels;
using Mamba.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AdminLoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminLoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser admin = null;
            admin = await _userManager.FindByNameAsync(adminLoginVM.Email);

            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }

        public async Task<IActionResult> CreateAdmin() 
        {
            AppUser user = null;

            user = new AppUser
            {
                UserName = "SuperAdmin",
                FullName = "Mehemmed",
            };
            var result =await _userManager.CreateAsync(user,"Admin123@");
            return Ok("yarandi");   
        }

        public async Task<IActionResult> CreateRole()
        {
            IdentityRole role1 = new IdentityRole("SuperAdmin");
            IdentityRole role2 = new IdentityRole("Admin");
            IdentityRole role3 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role1);
            await _roleManager.CreateAsync(role2);
            await _roleManager.CreateAsync(role3);

            return Ok("yarandi");
            
        }



       
    }
}
