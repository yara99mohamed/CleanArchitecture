using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<Role?> GetRoleByIdAsync(int id);
        Task<List<Role>?> GetRolesAsync();
        Task<string> AddRoleAsync(string roleName);
        Task<string> EditRoleAsync(EditRoleRequest data);
        Task<string> DeleteRoleAsync(int roleId);
        Task<bool> IsRoleExsitByNameAsync(string roleName);
        Task<bool> IsRoleExsitByIdAsync(int roleId);
        Task<(string, RolesByUserRequest?)> GetRolesByUserId(int userId);
        Task<string> UpdateRolesByUserId(RolesByUserRequest request);

        Task<(string, ClaimsByUserResult?)> GetClaimsByUserId(int userId);
        Task<string> UpdateClaimsByUserId(EditClaimsByUserRequest request);


    }
}
