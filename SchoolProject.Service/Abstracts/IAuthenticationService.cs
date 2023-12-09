using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthenticationResponse> GetJWTToken(User user);
        JwtSecurityToken ReadJwtToken(string accessToken);
        Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<JwtAuthenticationResponse> GetRefreshToken(User user, DateTime? expireDate, string refreshToken);
        Task<string> ValidateToken(string accessToken);
    }
}
