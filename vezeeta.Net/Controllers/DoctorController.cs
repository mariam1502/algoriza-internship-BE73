using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;
using vezeeta.Net.Models.ViewModel.Doctor;
using Data;
using System.Net;

namespace vezeeta.Net.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    [Route("[controller]/[action]")]
    [Authorize(Roles ="Doctor")]
    public class DoctorController:Controller
    {
        private IDoctorService doctorService;
        private readonly UserManager<IdentityUser> userManager;

        public DoctorController(IDoctorService doctorService, UserManager<IdentityUser> userManager) 
        {
            this.doctorService = doctorService;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Settings()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddAppointment()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentViewModel model)
        {
            var currentDoctorsEmail =  User.FindFirst(ClaimTypes.Email)?.Value;
            string currentDoctorId= (await  userManager.FindByEmailAsync(currentDoctorsEmail)).Id;
            if (currentDoctorId != null)
            {

                DoctorAppointment appointment = new DoctorAppointment()
                {
                    DoctorId = currentDoctorId,
                    Price = model.Price,
                };

                bool result = await doctorService.AddAppointment(appointment);
                if (result)
                {
                    return RedirectToAction("index", "Doctor");

                }
              
            }

           return NotFound();
            
        }

        [HttpPost]
        public async Task<IActionResult> AddDayTime(AppointmentViewModel model)
        {
            var currentDoctorsEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            string currentDoctorId = (await userManager.FindByEmailAsync(currentDoctorsEmail)).Id;

            Day day = new Day()
            {
                WeekDay = (Days)model.WeekDay,
            };
            Time time = new Time()
            {
                From=model.From,
                To=model.To,
            };
            bool result=await doctorService.AddDayTime(day, time, currentDoctorId, (Days)model.WeekDay);
            if (result)
            {
                return RedirectToAction("AddAppointment", "doctor");
            }
            //return Ok((Days)model.WeekDay);

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentDoctorsEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            string currentDoctorId = (await userManager.FindByEmailAsync(currentDoctorsEmail)).Id;
            IEnumerable<Patient>patients=await doctorService.GetAllPatientRequests(currentDoctorId);
            return View(patients);
            //return Ok(patients);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmCheckUp(int bookId)
        {
            bool result=await doctorService.ConfirmRequest(bookId);
            return RedirectToAction("Index","Doctor");
        }
    }
}
