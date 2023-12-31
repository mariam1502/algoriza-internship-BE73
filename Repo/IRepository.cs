﻿using Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public interface IRepository<T> where T : class
    {
       Task< IEnumerable<T>> GetAll();
        Task<bool> AddAsync(T user);
        Task<bool> DeleteAsync(T doctor);
        Task<bool> EditAsync(T user);


        Task<T> GetById(int int_id=0,string string_id="");
    }
}
