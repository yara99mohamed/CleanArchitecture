using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class DeleteRoleCommand : IRequest<Response<string>>
    {
        public DeleteRoleCommand(int id)
        {
            RoleId = id;
        }
        public int RoleId { get; set; }
    }
}
