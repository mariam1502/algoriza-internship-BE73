using Azure.Core;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Data;
using System.Security.Claims;
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
        [AllowAnonymous]
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
            ViewBag.GetAll = "Doctors";
            return View(doctors);
            //return Ok(doctors.First());
        }


        [HttpGet]
        public async Task<IActionResult> DoctorSchedule(int appointmentId)

        {
            IEnumerable<Day>  schedule=  await patientService.GetAllDoctorSchedule(appointmentId);
            return View(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Book(BookViewModel model)
        {

            var currentPatientEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            Patient currentPatient = (Patient)await userManager.FindByEmailAsync(currentPatientEmail);

            string currentPatientId = currentPatient.Id;

            Book bookData= new Book()
            {
                 PatientId=currentPatientId,
                 Request=model.Request,
                 TimeId=model.TimeId,
                 

                 
            };
            bool result=await patientService.Book(bookData);
            return RedirectToAction("index", "patient");
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooking()
        {
            IEnumerable<Book> books = await patientService.GetAllBooking();
            return View(books);

            //return Ok(books);
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            return Ok("canceled");
        }


    }
}
