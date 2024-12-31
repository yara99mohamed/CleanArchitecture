using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Feature.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthenticationResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
