using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthenticationResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
