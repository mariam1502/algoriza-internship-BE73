using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private DbSet<T> _entity;
        public Repository(ApplicationContext context) {

            this._context = context;
            _entity=context.Set<T>();
        }
        public void Add(T user)
        {
            _entity.Add(user);
        }

        public void Delete(long id)
        {
            T itemToDelete=_entity.FirstOrDefault<T>(e=>int.Parse(e.Id)==id);
            if ( itemToDelete != null )
            {
                _context.Remove(itemToDelete);
            }
        }

        public void Edit(T user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(long id)
        {
            T itemToGet = _entity.FirstOrDefault<T>(e => int.Parse(e.Id) == id);
            return itemToGet;
        }
    }
}
