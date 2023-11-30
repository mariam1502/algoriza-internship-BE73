using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using vezeeta.Net.Models.ViewModel.Admin;

namespace vezeeta.Net.Controllers
{
    [Route("[controller]/[action]")]
    public class PatientController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IPatientService patientService;

        public PatientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IPatientService patientService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.patientService = patientService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(PatientViewModel model)
        {
            Patient patient = new Patient()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.PasswordHash,
                Image = model.Image,
                Gendre = model.Gendre,
                Email = model.Email,
                NormalizedEmail = model.Email,

            };
            await patientService.Register(patient);
            if (!roleManager.RoleExistsAsync("Patient").Result)
            {
                var doctorRole = new IdentityRole("Patient");
                roleManager.CreateAsync(doctorRole).Wait();
            }
            userManager.AddToRoleAsync(patient, "Patient").Wait();
            return RedirectToAction("login","admin");
        
        }
    }
}
