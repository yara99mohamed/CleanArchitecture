using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Responses;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class GetRoleListQuery : IRequest<Response<List<GetRoleResponse>>>
    {
    }
}
