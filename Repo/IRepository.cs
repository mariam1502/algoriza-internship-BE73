using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        Task<T> GetByIdAsync(string id);
        Task AddAsync(T user);
        Task EditAsync(T user);
        Task DeleteAsync(T doctor);
    }
}
