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
using Microsoft.AspNetCore.Hosting;


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
        private readonly IWebHostEnvironment webHostEnvironment;
        private IEmailSender emailSenderService;   
        public AdminController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager ,IAdminService adminService, 
            RoleManager<IdentityRole> roleManager ,IWebHostEnvironment webHostEnvironment, IEmailSender emailSenderService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.adminService = adminService;
            this.roleManager = roleManager;
            this.webHostEnvironment = webHostEnvironment;
            this.emailSenderService = emailSenderService;
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
        private string UploadFile(AdminViewModel model)
        {
            string fileName = null;
            if(model.Image!=null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "doctorImages");
                fileName = Guid.NewGuid().ToString() + "-" + model.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    model.Image.CopyTo(fileStream); 
                }
            }
            return fileName;

        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor(AdminViewModel model)
        {
            string FileName = UploadFile(model);

            Doctor doctor = new Doctor()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PasswordHash = model.PasswordHash,
                specialization = model.specialization,
                Image = FileName,
                Gendre = model.Gendre,
                Email = model.Email,
                NormalizedEmail = model.Email,
                UserName=model.FirstName+model.LastName

            };


            bool addDoctorResult = await adminService.AddDoctor(doctor);
            if (addDoctorResult)
            {
                if (!await roleManager.RoleExistsAsync("Doctor"))
                {
                    var doctorRole = new IdentityRole("Doctor");
                    await roleManager.CreateAsync(doctorRole);
                }
                await userManager.AddToRoleAsync(doctor, "Doctor");
                string doctorId = (await userManager.FindByEmailAsync(model.Email)).Id;


                if(doctorId!=null)
                {
                    await emailSenderService.SendEmailAsync(doctorId);
                    return RedirectToAction("Index", "Admin");

                }


            }
            return BadRequest();
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
                ImageFileName = x.Image,
                Gendre = x.Gendre,
                DateOfBirth = x.DateOfBirth,
                PasswordHash = x.PasswordHash,
                NormalizedEmail = x.Email
            });
            ViewBag.GetAll = "Doctors";
            return View("getAll", models);
            //return Ok(models);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPatients(int? pageNumber)
        {
            IEnumerable<Patient> patients = await adminService.GetAllPatients(page: pageNumber ?? 1, pageSize: 2);
            IEnumerable<AdminViewModel> models = patients.Select(x => new AdminViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                ImageFileName = x.Image,
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
                Number=model.Number,
            };
            var result = await adminService.AddCoupon(coupon);
            if (result)
            {
                return RedirectToAction("index", "Admin");

            }
            return NotFound();
            //return Ok(result);
        }












    }
}
