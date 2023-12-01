using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationContext _context;
        private DbSet<T> _entity;

        public Repository(ApplicationContext context)
        {

            this._context = context;
            _entity = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _entity.AsEnumerable<T>();
        }

        public async Task<bool> AddAsync(T user)
        {
            _entity.Add(user);
            var result = await _context.SaveChangesAsync();
            return result>0;
        }

        public async Task<bool> DeleteAsync(T doctor)
        {
             _entity.Remove(doctor);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EditAsync(T newuser)
        {
            _entity.Update(newuser);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }



        public async Task<T> GetByIdAsync(string id)
        {
            //T itemToGet = await _entity.FirstOrDefaultAsync<T>(e => e.Id == id);
            //if(itemToGet != null)
            //{
            //    return itemToGet;
            //}
            throw new Exception($"Entity with ID {id} not found.");

        }


        public async Task<T> GetByEmailAsync(string email)
        {
            //T itemToGet = await _entity.FirstOrDefaultAsync<T>(e => e.Email == email);

            //if (itemToGet != null)
            //{
            //    return itemToGet;
            //}
            throw new Exception($"Entity with Email {email} not found.");

        }

        
    }


}
