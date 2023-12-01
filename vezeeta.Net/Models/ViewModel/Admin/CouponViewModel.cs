using Data;

namespace vezeeta.Net.Models.ViewModel.Admin
{
    public class CouponViewModel
    {
        public string Id { get; set; }
        public string CouponCode { get; set; }
        public int NumOfRequests { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
