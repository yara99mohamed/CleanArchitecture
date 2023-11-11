using MediatR;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Responses;
using SchoolProject.Core.wrappers;

namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Models
{
    public class GetApplicationUserPaginatedListQuery : IRequest<PaginatedResult<GetApplicationUserResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
