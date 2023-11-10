using MediatR;
using SchoolProject.Core.Feature.Students.Queries.Response;
using SchoolProject.Core.wrappers;
using SchoolProject.Data.Helper;

namespace SchoolProject.Core.Feature.Students.Queries.Request
{
    public class GetStudentPaginatedListQueryRequest : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentQrderingEnum? OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
