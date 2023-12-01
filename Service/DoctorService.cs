using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        private IRepository<DoctorAppointment> appointmentRepo;
        public DoctorService(IRepository<DoctorAppointment> appointmentRepo) 
        {
            this.appointmentRepo = appointmentRepo;
        }  
        public async Task<bool> AddAppointment(DoctorAppointment appointment)
        {
            bool result=await appointmentRepo.AddAsync(appointment);
            return result;
        }
    }
}
