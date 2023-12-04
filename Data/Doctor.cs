using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Doctor: BaseEntity
    {
        public string specialization {  get; set; }
        public string DoctorAppointmentId { get; set; }
        public DoctorAppointment DoctorAppointment { get; set; }
    }
}
