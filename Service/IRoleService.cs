using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IRoleService
    {
        Task<string> GetUserRoleById(string userId);

        Task<bool> AddUserRole(string userId, string role);
    }
}
