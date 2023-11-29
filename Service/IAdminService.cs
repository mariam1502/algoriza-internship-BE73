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
        int NumOfRequests();
        //IEnumerable<Request> Top10Requests();
        IEnumerable<Doctor> Top10Doctors();

        void AddDoctor(Doctor doctor);
        void EditDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
        IEnumerable<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);


        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int id);


        void AddCoupon(Coupon coupon);
        void EditCoupon(Coupon coupon);
        void DeleteCoupon(Coupon coupon);
        void DeactivateCoupon(Coupon coupon);



    }
}
