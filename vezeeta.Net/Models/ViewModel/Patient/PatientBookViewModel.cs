using Data;
using vezeeta.Net.Models.ViewModel.Admin;

namespace vezeeta.Net.Models.ViewModel.Patient
{
    public class PatientBookViewModel:AdminViewModel
    {
        public float Price { get; set; }  
        public int age { get; set; }
        
        public virtual  DoctorAppointment doctorAppointment { get; set; }


    }
}