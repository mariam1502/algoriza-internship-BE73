using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IDoctorService
    {
        //login

        //ConfirmCheckUp
        //ViewAllBooking

        Task<bool> AddAppointment(DoctorAppointment appointment);
        Task<bool> AddDayTime(Day day, Time time, string currentDrId ,Days wekkday);
        Task<IEnumerable<Patient>> GetAllPatientRequests(string doctorId);
        Task<bool> ConfirmRequest(int BookId);

        //Editappointment
        //Deleteappointment



    }
}
