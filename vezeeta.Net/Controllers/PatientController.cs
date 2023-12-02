using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Data;
using vezeeta.Net.Models.ViewModel.Admin;

namespace vezeeta.Net.Controllers
{
    //[ApiController]
    //[Route("[controller]")]

    [Route("[controller]/[action]")]
    public class PatientController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IPatientService patientService;
        private IRoleService patientRole;

        public PatientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IPatientService patientService, IRoleService patientRole)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.patientService = patientService;
            this.patientRole = patientRole;
            

        }
        [HttpGet]
        public IActionResult Index()
        {
            //return Ok("here");
            return RedirectToAction("GetAllDoctors", "Admin");
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
                UserName=model.FirstName+model.LastName,

            };
            var registerResult = await patientService.Register(patient);
            IdentityUser addedPatient = await userManager.FindByEmailAsync(patient.Email);

            if (registerResult && addedPatient != null)
            {
                if (!await roleManager.RoleExistsAsync("Patient"))
                {
                    var patientRole = new IdentityRole("Patient");
                    await roleManager.CreateAsync(patientRole);
                }
                var roleResult = await userManager.AddToRoleAsync(addedPatient, "Patient");

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("login", "admin");
                }

            }

            return NotFound();


        }
    }
}
