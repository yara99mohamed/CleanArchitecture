using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
        #endregion

        #region Constrsctors
        public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _jwtSettings = jwtSettings;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }
        #endregion

        #region Handle Functions
        public async Task<JwtAuthenticationResponse> GetJWTToken(User user)
        {
            var jwtToken = GenerateJWTToken(user);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(_jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                Token = accessToken,
                RefreshToken = refreshToken.Token,
                UserId = user.Id,
            };
            var createUserRefreshToken = await _userRefreshTokenRepository.AddAsync(userRefreshToken);
            //if (createUserRefreshToken == null) { 
            //// 

            //}
            var response = new JwtAuthenticationResponse { AccessToken = accessToken, RefreshToken = refreshToken };
            return response;
        }
        private List<Claim> GetClaims(string userName, string email, string phoneNumber)
        {
            var claims = new List<Claim>() {
            new Claim(nameof(UserClaimModel.UserName),userName),
            new Claim(nameof(UserClaimModel.Email),email),
            new Claim(nameof(UserClaimModel.PhoneNumber),phoneNumber),
            };
            return claims;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private JwtSecurityToken GenerateJWTToken(User user)
        {
            var jwtToken = new JwtSecurityToken(
              _jwtSettings.Issure,
               _jwtSettings.Audience,
               GetClaims(user.UserName, user.Email, user.PhoneNumber),
               expires: DateTime.UtcNow.AddDays(_jwtSettings.AccessTokenExpireDate),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            return jwtToken;
        }
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                UserName = userName,
                ExpireAt = DateTime.UtcNow.AddMonths(_jwtSettings.RefreshTokenExpireDate),
                Token = GenerateRefreshToken()
            };
            return refreshToken;
        }

        public Task<JwtAuthenticationResponse> GetRefreshToken(string accessToken, string refreshToken)
        {
            //Read Token To Get Cliams

            //Get 


        }
        #endregion
    }
}
