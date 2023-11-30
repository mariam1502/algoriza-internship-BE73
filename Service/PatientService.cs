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

        async Task  IPatientService.Register(Patient patient)
        {
            await patientRepo.AddAsync(patient);
            

        }
    }
}
