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

        public PatientService(IRepository<Patient> patientRepo, IRepository<Day> dayRepo) { 
            this.patientRepo = patientRepo;
            this.dayRepo = dayRepo;

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






    }
}
