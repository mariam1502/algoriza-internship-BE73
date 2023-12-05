using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public class Doctor: BaseEntity
    {
        public string specialization {  get; set; }
        public string DoctorAppointmentId { get; set; }
        [JsonIgnore]
        public virtual DoctorAppointment DoctorAppointment { get; set; }
    }
}
