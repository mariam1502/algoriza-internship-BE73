using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Day
    {
        public int Id { get; set; }
        public int DoctorAppointmentId { get; set; }
        public Days WeekDay { get; set; }


        public DoctorAppointment DoctorAppointment { get; set; }
        public List<Time> Times { get; set; }

    }
}
