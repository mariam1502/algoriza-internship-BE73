using Data;

namespace vezeeta.Net.Models.ViewModel.Doctor
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public Days Days { get; set; }
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public float Price { get; set; }

        public string DoctorId { get; set; }
    }
}
