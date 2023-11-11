using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Models
{
    public class DeleteApplicationUserCommand : IRequest<Response<string>>
    {
        public DeleteApplicationUserCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
