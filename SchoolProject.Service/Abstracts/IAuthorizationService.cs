using SchoolProject.Data.ViewData;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<string> EditRoleAsync(EditRoleViewData data);
        Task<bool> IsRoleExsitAsync(string roleName);
    }
}
