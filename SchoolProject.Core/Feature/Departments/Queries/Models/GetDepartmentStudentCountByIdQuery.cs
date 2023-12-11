using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Departments.Queries.Responses;

namespace SchoolProject.Core.Feature.Departments.Queries.Models
{
    public class GetDepartmentStudentCountByIdQuery : IRequest<Response<GetDepartmentStudentCountByIdResponse>>
    {
        public int Id { get; set; }
    }
}
