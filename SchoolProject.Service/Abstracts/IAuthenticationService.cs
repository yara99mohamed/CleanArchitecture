using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthenticationResponse> GetJWTToken(User user);
    }
}
