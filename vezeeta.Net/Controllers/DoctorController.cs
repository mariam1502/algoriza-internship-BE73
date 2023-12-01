using Data;
using Microsoft.AspNetCore.Mvc;
using Service;
using vezeeta.Net.Models.ViewModel.Doctor;

namespace vezeeta.Net.Controllers
{
    public class DoctorController:Controller
    {
        private IDoctorService doctorService;
        public DoctorController(IDoctorService doctorService) 
        {
            this.doctorService = doctorService;

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
            DoctorAppointment appointment = new DoctorAppointment()
            {
                Days = model.Days,
                DoctorId = model.DoctorId,
                From = model.From,  
                To = model.To,
                Price = model.Price
                 
            };

            bool result=await doctorService.AddAppointment(appointment);
            if(result)
            {
                return RedirectToAction("Settings", "Doctor");

            }
            else
            {
                return NotFound();
            }
        }
    }
}
