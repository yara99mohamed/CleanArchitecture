using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Feature.Authorization.Commands.Models
{
    public class UpdateClaimsUserCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public ICollection<Claims>? Claims { get; set; }
    }
}
