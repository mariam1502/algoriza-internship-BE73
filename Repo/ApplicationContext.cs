using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Repo
{
    public class ApplicationContext: IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Coupon> Coupons {  get; set; }
        public DbSet<DoctorAppointment> DoctorAppointments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();

            modelBuilder.Entity<Day>()
             .HasOne(b => b.DoctorAppointment)
             .WithMany(a => a.Days)
             .HasForeignKey(b => b.DoctorAppointmentId);

            modelBuilder.Entity<Time>()
             .HasOne(b => b.Day)
             .WithMany(a => a.Times)
             .HasForeignKey(b => b.DayId);

            modelBuilder.Entity<DoctorAppointment>()
            .HasOne(b => b.Doctor)
            .WithOne(a => a.DoctorAppointment)
            .HasForeignKey<DoctorAppointment>(b => b.DoctorId);


        }



    }
}
