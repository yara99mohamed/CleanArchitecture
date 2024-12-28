using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Responses;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class GetRolesByUserQuery : IRequest<Response<GetRolesByUserResponse>>
    {
        public GetRolesByUserQuery(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }
    }
}
