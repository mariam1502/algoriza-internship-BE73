using Data;

namespace vezeeta.Net.Models.ViewModel.Doctor
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public float Price { get; set; }
        public string DoctorId { get; set; }
        public int WeekDay { get; set; } // Change from List<int> to int
        public TimeOnly From { get; set; }  
        public TimeOnly To { get; set; }
     

    }
}
