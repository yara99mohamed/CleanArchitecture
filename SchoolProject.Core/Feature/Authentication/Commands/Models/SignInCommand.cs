using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthenticationResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
