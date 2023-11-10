using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Departments.Queries.Responses;

namespace SchoolProject.Core.Feature.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
    }
}
