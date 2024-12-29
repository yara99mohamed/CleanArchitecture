using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Responses;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class UpdateRolesUserCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
}
