using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthenticationResult> GetJWTToken(User user);
        JwtSecurityToken ReadJwtToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<JwtAuthenticationResult> GetRefreshToken(User user, DateTime? expireDate, string refreshToken);
        Task<string> ValidateToken(string accessToken);
    }
}
