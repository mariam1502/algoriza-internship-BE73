using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DoctorAppointment
    {
        public int Id { get; set; } 
        public Days Days { get; set; }  
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }
        public float Price { get; set; }
        public string DoctorId { get; set; }

        public Doctor Doctor { get; set; }

    }
}
