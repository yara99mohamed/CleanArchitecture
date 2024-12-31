
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service.Abstracts;
using System.Data;

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

        public async Task<string> EditRoleAsync(EditRoleRequest data)
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

        public async Task<(string, RolesByUserRequest?)> GetRolesByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ("NotFound", null);

            var response = new RolesByUserRequest();
            var rolesUser = new List<RoleRequest>();
            var rolesByUser = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in roles)
            {
                var userRole = new RoleRequest() { Id = role.Id, Name = role.Name, HasRole = rolesByUser.Contains(role.Name) };
                rolesUser.Add(userRole);
            }
            response.UserId = userId;
            response.UserName = user.UserName ?? "";
            response.Roles = rolesUser;
            return ("", response);
        }

        public async Task<string> UpdateRolesByUserId(RolesByUserRequest request)
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

        public async Task<(string, ClaimsByUserResult?)> GetClaimsByUserId(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return ("NotFound", null);

            var response = new ClaimsByUserResult();
            var claimsUser = new List<Claims>();

            var ClaimsByUser = await _userManager.GetClaimsAsync(user);
            //var Claims = await _roleManager.Roles.ToListAsync();
            foreach (var claim in ClaimsStore.claims)
            {
                ClaimsByUser.Any(x => x.Type == claim.Type);
                var claimUser = new Claims() { Name = claim.Type, Value = (ClaimsByUser.Any(x => x.Type == claim.Type)) };
                claimsUser.Add(claimUser);
            }

            response.UserId = userId;
            response.UserName = user.UserName ?? "";
            response.Claims = claimsUser;
            return ("", response);
        }

        public async Task<string> UpdateClaimsByUserId(EditClaimsByUserRequest request)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null) return "NotFound";

                var currentClaims = await _userManager.GetClaimsAsync(user);
                var removeResult = await _userManager.RemoveClaimsAsync(user, currentClaims);

                if (!removeResult.Succeeded) return "Failed To Remove Claims " + string.Join(", ", removeResult.Errors.Select(x => x.Description));

                var claimsToAdd = request.Claims?.Where(r => r.Value == true).Select(x => new System.Security.Claims.Claim(type: x.Name, value: x.Value.ToString()));

                var addResult = await _userManager.AddClaimsAsync(user, claimsToAdd);
                if (!addResult.Succeeded) return "Failed To Add Claims " + string.Join(", ", addResult.Errors.Select(x => x.Description));

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
