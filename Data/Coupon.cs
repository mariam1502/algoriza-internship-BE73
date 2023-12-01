using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Coupon
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public int NumOfRequests { get; set; }
        public int Number { get; set; }

        public DiscountType DisccountType { get; set; }


    }
}
