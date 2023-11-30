using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections;
using System.Runtime.CompilerServices;
using vezeeta.Net.Models.ViewModel.Admin;

namespace vezeeta.Net.Controllers
{
    [Route("[controller]/[action]")]
    public class AdminController:Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private IAdminService adminService;
        public AdminController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager ,IAdminService adminService, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.adminService = adminService;
            this.roleManager = roleManager;
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
            //if (ModelState.IsValid) 
            {
                IdentityUser user = await userManager.FindByEmailAsync(model.Email);
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



            //return Ok("Required");

        }

        [HttpGet]
        public IActionResult AddDoctor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(AdminViewModel model)
        {
            Doctor doctor = new Doctor()
            {
                FirstName = model.FirstName,    
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.PasswordHash,
                specialization=model.specialization,
                Image=model.Image,
                Gendre=model.Gendre,
                Email=model.Email,
                NormalizedEmail=model.Email

            };


            await adminService.AddDoctor(doctor);
            if (!await roleManager.RoleExistsAsync("Doctor"))
            {
                var doctorRole = new IdentityRole("Doctor");
                await roleManager.CreateAsync(doctorRole);
            }
            await userManager.AddToRoleAsync(doctor, "Doctor");
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            IEnumerable<Doctor> doctors = adminService.GetAllDoctors();
            IEnumerable<AdminViewModel> models = doctors.Select(x => new AdminViewModel()
            {
                Id= x.Id,
                Email=x.Email,
                FirstName=x.FirstName,
                LastName=x.LastName,
                PhoneNumber=x.PhoneNumber,
                specialization=x.specialization,
                Image=x.Image,
                Gendre=x.Gendre,
                DateOfBirth=x.DateOfBirth,
                PasswordHash=x.PasswordHash,
                NormalizedEmail = x.Email
            });
            ViewBag.GetAll = "Doctors";
            return View("getAll", models);
        }


        [HttpGet]
        public IActionResult GetAllPatients()
        {
            IEnumerable<Patient> patients = adminService.GetAllPatients();
            IEnumerable<AdminViewModel> models = patients.Select(x => new AdminViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Image = x.Image,
                Gendre = x.Gendre,
                DateOfBirth = x.DateOfBirth,
                PasswordHash = x.PasswordHash,
                NormalizedEmail=x.Email
            });
            ViewBag.GetAll = "Patients";
            return View("getAll", models);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(string id)
        {
            await adminService.DeleteDoctorAsync(id);
            return RedirectToAction("GetAllDoctors", "Admin");
        }


        [HttpPost]
        public async Task<IActionResult> EditDoctor(string id)
        {
            Doctor doctor = await adminService.GetDoctorById(id);
            //IEnumerable<Doctor> doctor=await adminService.GetDoctorById(id);

            return RedirectToAction("AddDoctor","Admin");
        }

        [HttpGet]
        public IActionResult GetNumOfDoctors()
        {
            return Ok(adminService.NumOfDoctors());

        }
        [HttpGet]
        public IActionResult GetNumOfPatients()
        {
            return Ok(adminService.NumOfPatients());

        }





    }
}
