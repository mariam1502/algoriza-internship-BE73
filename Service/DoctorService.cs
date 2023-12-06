using Data;
using Microsoft.AspNetCore.Identity;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        private IRepository<DoctorAppointment> appointmentRepo;
        private IRepository<Time> timeRepo;
        private IRepository<Day> dayRepo;
        private IRepository<Doctor> doctorRepo;
        private IRepository<Patient> patientRepo;
        private IRepository<Book> bookRepo;


        private readonly UserManager<IdentityUser> userManager;

        public DoctorService(IRepository<DoctorAppointment> appointmentRepo, IRepository<Time> timeRepo, IRepository<Day> dayRepo, IRepository<Doctor> doctorRepo, UserManager<IdentityUser> userManager, IRepository<Patient> patientRepo, IRepository<Book> bookRepo) 
        {
            this.appointmentRepo = appointmentRepo;
            this.timeRepo = timeRepo;
            this.dayRepo = dayRepo;
            this.doctorRepo = doctorRepo;
            this.userManager = userManager; 
            this.patientRepo = patientRepo;
            this.bookRepo = bookRepo;
        }  
        public async Task<bool> AddAppointment(DoctorAppointment appointment)
        {
            
            bool result = await appointmentRepo.AddAsync(appointment);
            if(result)
            {
                Doctor updatedDoctor =(Doctor)await userManager.FindByIdAsync(appointment.DoctorId);
                updatedDoctor.DoctorAppointmentId = appointment.Id.ToString();
                bool updateDoctorResult = await doctorRepo.EditAsync(updatedDoctor);
                if(updateDoctorResult)
                {
                    return true;
                }

            }
            return false;
        }
        public async Task<bool> AddDayTime(Day day , Time time,string currentDrId,Days weekday)
        {
            IEnumerable<DoctorAppointment> doctorAppointments = await appointmentRepo.GetAll();
            DoctorAppointment doctorAppointment=  doctorAppointments.FirstOrDefault(x => x.DoctorId == currentDrId);


            int doctorAppointmentId= day.DoctorAppointmentId = doctorAppointment.Id;
            bool dayResult = await dayRepo.AddAsync(day);

            if(dayResult)
            {
                IEnumerable<Day> days = await dayRepo.GetAll();
                Day currentDay = days.FirstOrDefault(x => x.DoctorAppointmentId == doctorAppointmentId && x.WeekDay == weekday);

                int currentDayId = currentDay.Id;
                time.DayId = currentDayId;  
                bool timeResult = await timeRepo.AddAsync(time);
                if (timeResult)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientRequests(string doctorId)
        {
            IEnumerable<Book>books=await bookRepo.GetAll();
            IEnumerable<Patient> patients = books.Where(x => x.Time.Day.DoctorAppointment.DoctorId == doctorId && x.Request==Request.Pending ).Select(x=>x.Patient);
            return patients;
        }

        public async Task<bool> ConfirmRequest(int BookId)
        {
            //bookRepo.GetByIdAsync()
            Book book =await bookRepo.GetById(BookId);
            if (book!=null)
            {
                book.Request = Request.Completed;
                bool result= await  bookRepo.EditAsync(book);
                return result;
               
            }
            return false;
        }

    }
}
