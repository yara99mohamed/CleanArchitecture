namespace SchoolProject.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<bool> IsRoleExsitAsync(string roleName);
    }
}
