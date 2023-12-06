using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public class DoctorAppointment
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string DoctorId { get; set; }

        [JsonIgnore]
        public virtual List<Day> Days { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
