using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public class Book
    {
        public int Id { get; set; }    
        public string PatientId { get; set; }
        public  int TimeId { get; set; }
        public  int? CouponId { get; set; }
        public Request Request { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Time Time { get; set; }
        public virtual Coupon Coupon { get; set; }  


    }
}
