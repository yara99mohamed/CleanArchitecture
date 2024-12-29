
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.ViewData;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly ApplicationDBContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constractors
        public AuthorizationService(ApplicationDBContext context, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _context = context;
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

        public async Task<(string, RolesByUserViewData?)> GetRolesByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ("NotFound", null);

            var response = new RolesByUserViewData();
            var rolesUser = new List<RoleViewData>();
            var rolesByUser = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRole = new RoleViewData() { Id = role.Id, Name = role.Name, HasRole = rolesByUser.Contains(role.Name) };
                rolesUser.Add(userRole);
            }
            response.UserId = userId;
            response.UserName = user.UserName ?? "";
            response.Roles = rolesUser;
            return ("", response);
        }

        public async Task<string> UpdateRolesByUserId(RolesByUserViewData request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "NotFound";

                var currentRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

                if (!removeResult.Succeeded) return "Failed To Remove Roles " + string.Join(", ", removeResult.Errors.Select(x => x.Description));

                var rolesToAdd = request.Roles?.Where(r => r.HasRole == true).Select(r => r.Name) ?? Enumerable.Empty<string>(); ;

                var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded) return "Failed To Add Roles " + string.Join(", ", addResult.Errors.Select(x => x.Description));

                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "Faild " + ex.Message;
            }

        }
        #endregion
    }
}
