
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        #endregion

        #region Constractors
        public AuthorizationService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }
        #endregion

        #region Functions
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role() { Name = roleName };
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded) return "Success";
            else return "Faild " + result.Errors.ToString();
        }

        public async Task<bool> IsRoleExsitAsync(string roleName)
        {
            //var role = await _roleManager.FindByNameAsync(roleName);
            //if (role != null) return true;
            //else return false;

            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion 
    }
}
