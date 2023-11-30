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
            IEnumerable<Doctor> doctors = doctorRepo.GetAll();
            int count = doctors.Count();
            return count;
        }

        public int NumOfPatients()
        {
            IEnumerable<Patient> patients = patientRepo.GetAll();
            int count = patients.Count();
            return count;
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

        async Task IAdminService.AddDoctor(Doctor doctor)
        {
            await doctorRepo.AddAsync(doctor);
            
        }

        void IAdminService.DeactivateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        void IAdminService.DeleteCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

         public async Task DeleteDoctorAsync(string id)
        {
           Doctor doctor=await doctorRepo.GetByIdAsync(id);
            if(doctor!=null)
            {
                await doctorRepo.DeleteAsync(doctor);

            }



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
            return doctorRepo.GetAll();
        }

        IEnumerable<Patient> IAdminService.GetAllPatients()
        {
            return patientRepo.GetAll();
        }

        Task<Doctor> IAdminService.GetDoctorById(string id)
        {
            return  doctorRepo.GetByIdAsync(id);
        }

        Patient IAdminService.GetPatientById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
