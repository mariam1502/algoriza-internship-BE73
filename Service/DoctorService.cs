using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        private IRepository<DoctorAppointment> appointmentRepo;
        private IRepository<Time> timeRepo;
        private IRepository<Day> dayRepo;


        public DoctorService(IRepository<DoctorAppointment> appointmentRepo, IRepository<Time> timeRepo, IRepository<Day> dayRepo) 
        {
            this.appointmentRepo = appointmentRepo;
            this.timeRepo = timeRepo;
            this.dayRepo = dayRepo;
        }  
        public async Task<bool> AddAppointment(DoctorAppointment appointment)
        {
            bool result = await appointmentRepo.AddAsync(appointment);
            return result;
        }
        public async Task<bool> AddDayTime(Day day , Time time,string currentDrId,Days weekday)
        {
            IEnumerable<DoctorAppointment> doctorAppointments = await appointmentRepo.GetAll();
            DoctorAppointment doctorAppointment=  doctorAppointments.FirstOrDefault(x => x.DoctorId == currentDrId);


            int doctorAppointmentId= day.DoctorAppointmentId = doctorAppointment.Id;
            bool dayResult = await dayRepo.AddAsync(day);

            if(dayResult)
            {
                IEnumerable<Day> days = await dayRepo.GetAll();
                Day currentDay = days.FirstOrDefault(x => x.DoctorAppointmentId == doctorAppointmentId && x.WeekDay == weekday);

                int currentDayId = currentDay.Id;
                time.DayId = currentDayId;  
                bool timeResult = await timeRepo.AddAsync(time);
                if (timeResult)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
