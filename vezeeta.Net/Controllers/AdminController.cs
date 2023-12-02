using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Linq;
using vezeeta.Net.Models.ViewModel.Admin;

namespace vezeeta.Net.Controllers
{
    //[ApiController]
    //[Route("[controller]")]

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
        [HttpGet]
        public IActionResult Index()
        {
            //return Ok("here");
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {

            DashboardModelView model = new DashboardModelView()
            {
                NumOfDoctors = adminService.NumOfDoctors(),
                NumOfPatients = adminService.NumOfPatients(),
                NumOfRequests = 0,
                NumOfCompletedRequests = 0,
                Top5Specializations = new List<dynamic>(),
                Top10Doctors = new List<Doctor>(),
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            //adminService.NumOfDoctors()
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Login(AdminViewModel model)
        {
            //if (ModelState.IsValid) 
            {
                IdentityUser user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (user.PasswordHash == model.PasswordHash)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        var roles = userManager.GetRolesAsync(user);
                        string userRole = roles.Result.FirstOrDefault();

                        return View("~/Views/" + userRole.ToString() + "/index.cshtml", user);



                        //return View("Views/Admin/index.cshtml", user);
                        //return Ok(user);
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
                specialization = model.specialization,
                Image = model.Image,
                Gendre = model.Gendre,
                Email = model.Email,
                NormalizedEmail = model.Email

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
        public IActionResult GetAllDoctors(int? pageNumber)
        {
            IEnumerable<Doctor> doctors = adminService.GetAllDoctors(page: pageNumber ?? 1, pageSize: 2);
            IEnumerable<AdminViewModel> models = doctors.Select(x => new AdminViewModel()
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
                NormalizedEmail = x.Email
            });
            ViewBag.GetAll = "Patients";
            return View("getAll", models);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(string Id)
        {
            Doctor doctor = adminService.GetAllDoctors().FirstOrDefault(x => x.Id == Id);
            bool result = await adminService.DeleteDoctorAsync(doctor);
            if (result)
            {
                return RedirectToAction("GetAllDoctors", "Admin");

            }
            else
            {
                return NotFound();
            }
        }



        [HttpPost]
        public async Task<IActionResult> EditDoctor(string id)
        {
            Doctor doctor = adminService.GetAllDoctors().FirstOrDefault(x => x.Id == id);
            bool result = await adminService.EditDoctor(doctor);
            if (result)
            {
                return RedirectToAction("GetAllDoctors", "Admin");

            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCoupon()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> AddCoupon(CouponViewModel model)
        {
            Coupon coupon = new Coupon()
            {
                CouponCode = model.CouponCode,
                DisccountType = model.DiscountType,
                NumOfRequests = model.NumOfRequests,
            };
            var result = await adminService.AddCoupon(coupon);
            if (result)
            {
                return View();

            }
            return NotFound();
            //return Ok(result);
        }












    }
}
