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
        public Repository(ApplicationContext context)
        {

            this._context = context;
            _entity = context.Set<T>();
        }
        public void Add(T user)
        {
            _entity.Add(user);
        }

        public void Delete(int id)
        {
            T itemToDelete = _entity.FirstOrDefault(e => int.Parse(e.Id) == id);

            if (itemToDelete != null)
            {
                _entity.Remove(itemToDelete);
            }
        }

        public void Edit(T user)
        {
            T getuser = _entity.FirstOrDefault<T>(u => int.Parse(u.Id) == int.Parse(user.Id));
            if (getuser != null)
            {
                getuser.Id = user.Id;
                getuser.PhoneNumber = user.PhoneNumber;
                getuser.FirstName = user.FirstName;
                getuser.LastName = user.LastName;
                getuser.Email = user.Email;
                getuser.Gendre = user.Gendre;
                getuser.Image = user.Image;
                getuser.PasswordHash = user.PasswordHash;
            }


        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable<T>();
        }

        public T GetById(int id)
        {
            T itemToGet = _entity.FirstOrDefault<T>(e => int.Parse(e.Id) == id);
            return itemToGet;
        }
    }

       
}
