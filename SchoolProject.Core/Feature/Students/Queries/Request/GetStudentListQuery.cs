using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Response;

namespace SchoolProject.Core.Feature.Students.Queries.Request
{
    public class GetStudentListQuery:IRequest<Response<List<GetStudentListResponse>>>
    {
    }
}
