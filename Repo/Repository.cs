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
        public async Task AddAsync(T user)
        {
            _entity.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T doctor)
        {
            _entity.Remove(doctor);
            await _context.SaveChangesAsync();

            
        }

        public async Task EditAsync(T newuser)
        {
            T getuser = await GetByIdAsync(newuser.Id);
            if (getuser != null)
            {
                getuser.PhoneNumber = newuser.PhoneNumber;
                getuser.FirstName = newuser.FirstName;
                getuser.LastName = newuser.LastName;
                getuser.Email = newuser.Email;
                getuser.Gendre = newuser.Gendre;
                getuser.Image = newuser.Image;
                getuser.PasswordHash = newuser.PasswordHash;
                getuser.DateOfBirth = newuser.DateOfBirth; 
                
            }
            _entity.Update(getuser);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable<T>();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            T itemToGet =await  _entity.FirstOrDefaultAsync<T>(e => e.Id == id);

            return itemToGet;
        }
    }

       
}
