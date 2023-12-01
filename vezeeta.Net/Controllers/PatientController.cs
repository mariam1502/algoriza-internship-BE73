using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Data;
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
        private IRoleService patientRole;

        public PatientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IPatientService patientService, IRoleService patientRole)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.patientService = patientService;
            this.patientRole = patientRole;

        }
        public IActionResult Index()
        {
            return RedirectToAction("GetAllDoctors","Admin");
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
            string patientId = (await userManager.FindByEmailAsync(patient.Email)).Id;
            ////await patientRole.AddUserRole(patientId, "Patient");
            IdentityUser p = await userManager.FindByIdAsync(patientId);
            //IdentityResult pp=await userManager.AddToRoleAsync(p, "patient");


            //return Ok(await patientService.test(patient));


            if (!roleManager.RoleExistsAsync("Patient").Result)
            {
                var doctorRole = new IdentityRole("Patient");
                roleManager.CreateAsync(doctorRole).Wait();
            }
            IdentityResult result =await  userManager.AddToRoleAsync(p, "PATIENT");
           
          
            return Ok(result.Errors);

            //userManager.AddToRoleAsync(patient, "Patient").Wait();

        }
    }
}
