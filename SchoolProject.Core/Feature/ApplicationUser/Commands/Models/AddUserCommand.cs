using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Responses;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Models
{
    public class AddUserCommand : IRequest<Response<AddUserResponse>>
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
