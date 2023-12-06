using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PatientService:IPatientService
    {
        private IRepository<Patient> patientRepo;
        private IRepository<Day> dayRepo;
        private IRepository<Book> bookRepo;

        public PatientService(IRepository<Patient> patientRepo, IRepository<Day> dayRepo, IRepository<Book> bookRepo) { 
            this.patientRepo = patientRepo;
            this.dayRepo = dayRepo;
            this.bookRepo = bookRepo;

        }


        Task IPatientService.login(Patient patient)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IPatientService.Register(Patient patient)
        {
            var result=await patientRepo.AddAsync(patient);

            if (result)
            {
                return true;
            }
            return false;

        }
        public async Task<IEnumerable<Day>> GetAllDoctorSchedule(int appointmentId)
        {
            IEnumerable<Day> days= await dayRepo.GetAll();
            IEnumerable<Day> specificDoctorDays = days.Where(s => s.DoctorAppointmentId == appointmentId);
            return specificDoctorDays;    
            
        }

        public async Task<bool> Book(Book bookData)
        {
            bool result=await bookRepo.AddAsync(bookData);
            return result;
        }

        public async Task<IEnumerable<Book>> GetAllBooking()
        {
            IEnumerable<Book> books = await bookRepo.GetAll();
            return books;

        }





    }
}
