using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPatientService
    {
        Task<bool> Register(Patient patient);
        Task login(Patient patient);
        Task<IEnumerable<Day>> GetAllDoctorSchedule(int appointmentId);


        //IEnumerable<Doctor> GetAllDoctors(int page = 1, int pageSize = 10);  //duplicated method



    }
}
