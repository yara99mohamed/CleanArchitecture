using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Responses;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleResponse>>
    {
        public GetRoleByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
