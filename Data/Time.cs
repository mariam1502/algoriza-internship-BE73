using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data
{
    public class Time
    {
        public int Id { get; set; } 
        public int DayId { get; set; }   
        public TimeOnly From { get; set; }
        public TimeOnly To { get; set; }

        [JsonIgnore]
        public virtual Day Day { get; set; }


    }
}
