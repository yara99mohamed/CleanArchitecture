using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Results;

namespace SchoolProject.Core.Feature.Authorization.Queries.Models
{
    public class GetClaimsByUserQuery : IRequest<Response<ClaimsByUserResult>>
    {
        public int UserId { get; set; }
    }
}
