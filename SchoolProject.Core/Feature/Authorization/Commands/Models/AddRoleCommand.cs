using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public required string RoleName { get; set; }
    }
}
