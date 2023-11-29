using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;

namespace Service
{
    public class AdminService : IAdminService
    {
        private IRepository<Doctor> doctorRepo;
        private IRepository<Patient> patientRepo;
        public AdminService(IRepository<Doctor> doctorRepo, IRepository<Patient> patientRepo) 
        { 
            this.doctorRepo = doctorRepo;
            this.patientRepo = patientRepo;
        }
        public int NumOfDoctors()
        {
            return doctorRepo.GetAll().Count();
        }

        public int NumOfPatients()
        {
            return patientRepo.GetAll().Count();
        }

        public int NumOfRequests()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> Top10Doctors()
        {
            throw new NotImplementedException();
        }

        void IAdminService.AddCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        void IAdminService.AddDoctor(Doctor doctor)
        {
            doctorRepo.Add(doctor);
        }

        void IAdminService.DeactivateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        void IAdminService.DeleteCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        void IAdminService.DeleteDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        void IAdminService.EditCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        void IAdminService.EditDoctor(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Doctor> IAdminService.GetAllDoctors()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Patient> IAdminService.GetAllPatients()
        {
            throw new NotImplementedException();
        }

        Doctor IAdminService.GetDoctorById(int id)
        {
            throw new NotImplementedException();
        }

        Patient IAdminService.GetPatientById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
