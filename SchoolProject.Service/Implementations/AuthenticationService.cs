using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constrsctors
        public AuthenticationService(JwtSettings jwtSettings, IUserRefreshTokenRepository userRefreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public async Task<JwtAuthenticationResponse> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = GenerateJWTToken(user);

            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
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

        private List<Claim> GetClaims(int id, string userName, string email, string phoneNumber)
        {
            var claims = new List<Claim>() {
            new Claim(nameof(UserClaimModel.Id),id.ToString()),
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

        private (JwtSecurityToken, string) GenerateJWTToken(User user)
        {
            var jwtToken = new JwtSecurityToken(
              _jwtSettings.Issure,
               _jwtSettings.Audience,
               GetClaims(user.Id, user.UserName, user.Email, user.PhoneNumber),
               expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDate),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }

        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                UserName = userName,
                ExpireAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDate),
                Token = GenerateRefreshToken()
            };
            return refreshToken;
        }

        public async Task<JwtAuthenticationResponse> GetRefreshToken(User user, DateTime? expireDate, string refreshToken)
        {
            //Generate New Token
            var (jwtSecurityToken, newAccessToken) = GenerateJWTToken(user);
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = user.UserName;
            refreshTokenResult.ExpireAt = (DateTime)expireDate;
            refreshTokenResult.Token = refreshToken;
            return new JwtAuthenticationResponse { AccessToken = newAccessToken, RefreshToken = refreshTokenResult };
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            //Validation Token , Refresh Token
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                return ("AlgorithmIsWrong", null);

            if (jwtToken.ValidTo > DateTime.UtcNow)
                return ("TokenIsNotExpired", null);


            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

            var userRefreshToken = await _userRefreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.Token.Equals(accessToken) &&
                                           x.RefreshToken.Equals(refreshToken) &&
                                           x.UserId.Equals(int.Parse(userId)));

            if (userRefreshToken == null)
                return ("RefreshTokenIsNotFound", null);

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _userRefreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenIsNotExpired", null);
            }
            var expireDate = userRefreshToken.ExpiryDate;
            return (userId, expireDate);
        }

        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssure,
                ValidIssuers = new[] { _jwtSettings.Issure },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssureSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime
            };
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
            try
            {
                if (validator == null)
                    return "InvalideToken";
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
