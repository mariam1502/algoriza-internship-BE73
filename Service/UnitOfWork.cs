using Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _Context;

        public UnitOfWork(ApplicationContext Context)
        {
            _Context = Context;
        }
        void IDisposable.Dispose()
        {
            _Context.Dispose();
        }

        public async Task<bool> SaveChanges()
        {
            var result= await _Context.SaveChangesAsync();
            return  result > 0;

        }
    }
}
