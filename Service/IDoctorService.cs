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
        //Editappointment
        //Deleteappointment



    }
}
