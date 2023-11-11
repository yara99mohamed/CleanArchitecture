using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Responses;

namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Models
{
    public class GetApplicationUserByIdQuery : IRequest<Response<GetApplicationUserResponse>>
    {
        public GetApplicationUserByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
