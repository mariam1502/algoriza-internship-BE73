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
        private readonly IUnitOfWork unitOfWork;
        private DbSet<T> _entity;

        public Repository(ApplicationContext context, IUnitOfWork unitOfWork)
        {

            this._context = context;
            this.unitOfWork = unitOfWork;
            _entity = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<bool> AddAsync(T user)
        {
            _entity.Add(user);
            var result =await unitOfWork.SaveChanges();
            return result;
        }

        public async Task<bool> DeleteAsync(T doctor)
        {
             _entity.Remove(doctor);
            var result =await unitOfWork.SaveChanges();
            return result;
        }

        public async Task<bool> EditAsync(T newuser)
        {
            _entity.Update(newuser);
            var result =await unitOfWork.SaveChanges();
            return result;
        }



        public async Task<T> GetById(int int_id=0, string string_id= "")
        {
            string tableName = _context.Model.FindEntityType(typeof(T)).GetTableName();
            string sql = " ";
            if (int_id != 0)
            {
               sql = $"SELECT * FROM {tableName} WHERE Id = '{int_id}'";
            }
            if(string_id !="") 
            {
                 sql = $"SELECT * FROM {tableName} WHERE Id = '{string_id}'";
            }
            T item = await _entity.FromSqlRaw(sql).FirstOrDefaultAsync();
            if (item != null)
            {
                return item;
            }

           
            throw new Exception($"Entity  not found.");

        }

        
    }


}
