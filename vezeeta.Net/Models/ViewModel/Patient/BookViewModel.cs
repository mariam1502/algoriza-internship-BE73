using Data;

namespace vezeeta.Net.Models.ViewModel.Patient
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public int TimeId { get; set; }
        public int? CouponId { get; set; }
        public Request Request { get; set; }




    }
}
