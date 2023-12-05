using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Data;
using vezeeta.Net.Models.ViewModel.Admin;
using vezeeta.Net.Models.ViewModel.Patient;

namespace vezeeta.Net.Controllers
{
    //[ApiController]
    //[Route("[controller]")]

    [Route("[controller]/[action]")]
    [Authorize(Roles = "Patient")]

    public class PatientController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IPatientService patientService;
        private readonly IAdminService adminService;
        private IRoleService patientRole;

        public PatientController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IPatientService patientService, IRoleService patientRole, IAdminService adminService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.patientService = patientService;
            this.patientRole = patientRole;
            this.adminService = adminService;
            

        }
        [HttpGet]
        public IActionResult Index()
        {
            //return Ok("here");
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
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

        [HttpGet]
        public  async Task<IActionResult> GetAllDoctors(int? pageNumber)
        {
            IEnumerable<Doctor> doctors =  await adminService.GetAllDoctors(page: pageNumber ?? 1, pageSize: 2);
            IEnumerable<PatientBookViewModel> models = doctors.Select(x => new PatientBookViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                specialization = x.specialization,
                Image = x.Image,
                Gendre = x.Gendre,
                DateOfBirth = x.DateOfBirth,
                PasswordHash = x.PasswordHash,
                NormalizedEmail = x.Email,
                

            });
            ViewBag.GetAll = "Doctors";
            return View(doctors);
            //return Ok(doctors.First());
        }


        [HttpGet]
        public IActionResult DoctorSchedule()
        {
            //IEnumerable<Doctor> doctors = adminService.GetAllDoctors(page: pageNumber ?? 1, pageSize: 2);
            //IEnumerable<PatientBookViewModel> models = doctors.Select(x => new PatientBookViewModel()
            //{
            //    Id = x.Id,
            //    Email = x.Email,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    PhoneNumber = x.PhoneNumber,
            //    specialization = x.specialization,
            //    Image = x.Image,
            //    Gendre = x.Gendre,
            //    DateOfBirth = x.DateOfBirth,
            //    PasswordHash = x.PasswordHash,
            //    NormalizedEmail = x.Email


            //});
            //ViewBag.GetAll = "Doctors";
            //return Ok("hello");
            return View();
        }


    }
}
