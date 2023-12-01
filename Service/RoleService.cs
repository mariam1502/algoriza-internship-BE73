using Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class RoleService:IRoleService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RoleService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


         public async Task<bool> AddUserRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            var result = await _userManager.AddToRoleAsync(user, role);

            return result.Succeeded;
        }


         public async Task<string> GetUserRoleById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);

            return roles.FirstOrDefault();
        }


    }


   
    
}
