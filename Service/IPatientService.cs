using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IPatientService
    {
        Task Register(Patient patient);
        Task login(Patient patient);


    }
}
