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

        Task<int> NumOfDoctors();
        Task<int> NumOfPatients();

        Task<bool> AddDoctor(Doctor doctor);
        Task<bool> EditDoctor(Doctor doctor);
        Task<bool> DeleteDoctorAsync(Doctor doctor);
        Task<IEnumerable<Doctor>>GetAllDoctors(int page=1 , int pageSize=10);  //duplicated method
        //Task<Doctor> GetDoctorById(string id);


        Task<IEnumerable<Patient>> GetAllPatients();
        //Task<Patient> GetPatientById(string id);


        Task<bool> AddCoupon(Coupon coupon);
        Task<bool>EditCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(Coupon coupon);

        Task<bool> DeactivateCoupon(Coupon coupon);



    }
}
