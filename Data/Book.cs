using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Book
    {
        public int Id { get; set; }    
        public virtual Time Time { get; set; }  
        public virtual Coupon Coupon { get; set; }  
        public string Request { get; set; }
    }
}
