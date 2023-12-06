using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repo;
using System.Numerics;
using Azure;

namespace Service
{
    public class AdminService : IAdminService
    {
        private IRepository<Doctor> doctorRepo;
        private IRepository<Patient> patientRepo;
        private IRepository<Coupon> couponRepo;

        public AdminService(IRepository<Doctor> doctorRepo, IRepository<Patient> patientRepo, IRepository<Coupon> couponRepo) 
        { 
            this.doctorRepo = doctorRepo;
            this.patientRepo = patientRepo;
            this.couponRepo = couponRepo;
        }
        public async Task<int> NumOfDoctors()
        {
            IEnumerable<Doctor> doctors = await doctorRepo.GetAll();
            int count = doctors.Count();
            return count;
        }

        public async Task<int> NumOfPatients()
        {
            IEnumerable<Patient> patients =  await patientRepo.GetAll();
            int count = patients.Count();
            return count;
        }
        public async Task<bool> AddDoctor(Doctor doctor)
        {
            bool result=await doctorRepo.AddAsync(doctor);
            return result;
        }
        public async Task<bool> EditDoctor(Doctor doctor)
        {
            bool result = await doctorRepo.EditAsync(doctor);
            return result;
        }


        public async Task<bool> DeleteDoctorAsync(Doctor doctor)
        {
            bool result = await doctorRepo.DeleteAsync(doctor);
            return result;
        }

        public async Task<IEnumerable<Doctor> >GetAllDoctors(int page, int pageSize)
        {
            IEnumerable<Doctor> doctors =await  doctorRepo.GetAll();          
            doctors = doctors.Skip((page - 1) * pageSize).Take(pageSize);
            return doctors;   
        }
      
      
        public async Task<IEnumerable<Patient>> GetAllPatients(int page, int pageSize)
        {

            IEnumerable<Patient> patients = await patientRepo.GetAll();
            patients = patients.Skip((page - 1) * pageSize).Take(pageSize);
            return patients;
        }

      

        public async Task<bool> AddCoupon(Coupon coupon)
        {
            bool result=await couponRepo.AddAsync(coupon);
            return result;
        }

        public async Task<bool> EditCoupon(Coupon coupon)
        {
            bool result = await couponRepo.EditAsync(coupon);
            return result;
        }

        public async  Task<bool> DeleteCoupon(Coupon coupon)
        {
            bool result = await couponRepo.AddAsync(coupon);
            return result;
        }

        Task<bool> IAdminService.DeactivateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
