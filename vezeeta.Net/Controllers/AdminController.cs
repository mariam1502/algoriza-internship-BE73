using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using vezeeta.Net.Models.ViewModel.Admin;

namespace vezeeta.Net.Controllers
{
    [Route("[controller]/[action]")]

    public class AdminController:Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private IAdminService adminService;
        public AdminController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager ,IAdminService adminService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.adminService = adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login( )
        {
            //adminService.NumOfDoctors()
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Login( AdminViewModel model)
        {
            if (ModelState.IsValid) {
                IdentityUser user = await userManager.FindByIdAsync("1");
                if (user != null)
                {
                    if (user.PasswordHash == model.PasswordHash)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return View("Views/Admin/index.cshtml", user);
                    }
                    return Ok("Password incorrect");
                }
                return Ok("User NOt Found!");
            }



            return Ok("Required");

        }

        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }



    }
}
