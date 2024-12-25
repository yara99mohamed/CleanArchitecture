
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.ViewData;
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
            else return "Faild " + string.Join("- ", result.Errors);
        }

        public async Task<string> EditRoleAsync(EditRoleViewData data)
        {
            var role = await _roleManager.FindByIdAsync(data.Id.ToString());
            if (role == null) return "NotFound";

            role.Name = data.Name;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded) return "Success";
            else return "Faild " + string.Join("- ", result.Errors);
        }

        public async Task<bool> IsRoleExsitAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion
    }
}
