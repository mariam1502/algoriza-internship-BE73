using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Linq;
using vezeeta.Net.Models.ViewModel.Admin;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;


namespace vezeeta.Net.Controllersf
{
    //[ApiController]
    //[Route("[controller]")]

    [Route("[controller]/[action]")]
    [Authorize(Roles ="Admin")]
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

        public async Task<IActionResult> Index()
        {
            //return Ok("here");
            //await userManager.GetClaimsAsync(user);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {

            DashboardModelView model = new DashboardModelView()
            {
                NumOfDoctors = await adminService.NumOfDoctors(),
                NumOfPatients = await adminService.NumOfPatients(),
                NumOfRequests = 0,
                NumOfCompletedRequests = 0,
                Top5Specializations = new List<dynamic>(),
                Top10Doctors = new List<Doctor>(),
            };
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            //adminService.NumOfDoctors()
            return View();

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AdminViewModel model)
        {
            //if (ModelState.IsValid) 
            {
                IdentityUser user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {

                    if (user.PasswordHash == model.PasswordHash)
                    {
                        Claim claim = new Claim("Id", user.Id);
                        var result = await userManager.AddClaimAsync(user, claim);

                        await signInManager.SignInAsync(user, isPersistent: true);
                        var roles = userManager.GetRolesAsync(user);
                        string userRole = roles.Result.FirstOrDefault();
                        var calim =await userManager.GetClaimsAsync(user);

                        //return Ok(claim.Value);
                        return View("~/Views/" + userRole.ToString() + "/index.cshtml");
                        //return RedirectToAction("index", "admin");
                    }

                    return Ok("Password incorrect");
                }
                return Ok("User NOt Found!");
            }

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
                NormalizedEmail = model.Email,
                UserName=model.FirstName+model.LastName

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
        public async Task<IActionResult> GetAllDoctors(int? pageNumber)
        {
            IEnumerable<Doctor> doctors = await adminService.GetAllDoctors(page: pageNumber ?? 1, pageSize: 2);
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
        public async Task<IActionResult> GetAllPatients()
        {
            IEnumerable<Patient> patients = await adminService.GetAllPatients();
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
            IEnumerable<Doctor> doctors = await adminService.GetAllDoctors();

            // Now you can use FirstOrDefault on the result
            Doctor doctor = doctors.FirstOrDefault(x => x.Id == Id);

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
            IEnumerable<Doctor> doctors = await adminService.GetAllDoctors();

            Doctor doctor = doctors.FirstOrDefault(x => x.Id == id);
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
