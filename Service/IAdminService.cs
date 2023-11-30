using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Repo.Migrations;
namespace Service
{
    public interface IAdminService
    {

        int NumOfDoctors();
        int NumOfPatients();

        Task AddDoctor(Doctor doctor);
        void EditDoctor(Doctor doctor);
        Task DeleteDoctorAsync(string id);
        IEnumerable<Doctor> GetAllDoctors();
        Task<Doctor> GetDoctorById(string id);


        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(string id);


        void AddCoupon(Coupon coupon);
        void EditCoupon(Coupon coupon);
        void DeleteCoupon(Coupon coupon);
        void DeactivateCoupon(Coupon coupon);



    }
}
