using Data;
using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class PatientService:IPatientService
    {
        private IRepository<Patient> patientRepo;
        public PatientService(IRepository<Patient> patientRepo) { 
            this.patientRepo = patientRepo;
            

        }


        Task IPatientService.login(Patient patient)
        {
            throw new NotImplementedException();
        }
        public async Task<string> test(Patient patient)
        {
             string patientId = (await patientRepo.GetByEmailAsync(patient.Email)).Id;
            return  patientId;
        }


        async Task<bool> IPatientService.Register(Patient patient)
        {
            var result=await patientRepo.AddAsync(patient);

            if (result)
            {
                return true;
            }
            return false;

        }





    }
}
