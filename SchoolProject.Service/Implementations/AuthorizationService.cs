
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.ViewData;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constractors
        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #endregion

        #region Functions
        public async Task<Role?> GetRoleByIdAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            return role;
        }

        public async Task<List<Role>?> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

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

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null) return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users != null && users.Count > 0) return "Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded) return "Success";
            else return "Faild " + string.Join("- ", result.Errors);
        }

        public async Task<bool> IsRoleExsitByIdAsync(int roleId)
        {
            return await _roleManager.FindByIdAsync(roleId.ToString()) != null;
        }

        public async Task<bool> IsRoleExsitByNameAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<(string, GetRolesByUserViewData?)> GetRolesByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ("NotFound", null);

            var response = new GetRolesByUserViewData();
            var rolesUser = new List<UserRolesViewData>();
            var rolesByUser = _userManager.GetRolesAsync(user).Result;
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRole = new UserRolesViewData() { Id = role.Id, Name = role.Name, HasRole = rolesByUser.Contains(role.Name) };
                rolesUser.Add(userRole);
            }
            response.UserId = userId;
            response.UserName = user.UserName ?? "";
            response.Roles = rolesUser;
            return ("", response);
        }
        #endregion
    }
}
