using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthenticationResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
