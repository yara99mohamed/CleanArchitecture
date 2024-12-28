using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.ViewData;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<Role?> GetRoleByIdAsync(int id);
        Task<List<Role>?> GetRolesAsync();
        Task<string> AddRoleAsync(string roleName);
        Task<string> EditRoleAsync(EditRoleViewData data);
        Task<string> DeleteRoleAsync(int roleId);
        Task<bool> IsRoleExsitByNameAsync(string roleName);
        Task<bool> IsRoleExsitByIdAsync(int roleId);
        Task<(string, GetRolesByUserViewData?)> GetRolesByUserId(int userId);
    }
}
