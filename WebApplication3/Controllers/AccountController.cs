using Mamba.Core.Models;
using Mamba.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mamba.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,   
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
           
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = null;

            user = await _userManager.FindByNameAsync(memberRegisterVM.Username);
            if (user != null)
            {
                ModelState.AddModelError("Username", "Username already exist");
                return View();
            }

            user = await _userManager.FindByEmailAsync(memberRegisterVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("Email", "Email already exist");
                return View();
            }
            AppUser user1 = new AppUser
            {
                FullName = memberRegisterVM.Fullname,
                UserName = memberRegisterVM.Username,
                Email = memberRegisterVM.Email,
                
            };

            var result = await _userManager.CreateAsync(user1, memberRegisterVM.Password);

            
            await _userManager.AddToRoleAsync(user1, "Member");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = null;
            user = await _userManager.FindByEmailAsync(memberLoginVM.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, memberLoginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
