using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Models;
using SchoolProject.Core.Feature.Authorization.Queries.Responses;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authorization.Queries.Handlers
{
    public class RolesUserQueryHandler : ResponseHandler, IRequestHandler<GetRolesByUserQuery, Response<GetRolesByUserResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractors
        public RolesUserQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationService authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetRolesByUserResponse>> Handle(GetRolesByUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.GetRolesByUserId(request.UserId);
            if (result.Item1 == "NotFound") return NotFound<GetRolesByUserResponse>("User Is Not Exist");
            var response = _mapper.Map<GetRolesByUserResponse>(result.Item2);
            return Success<GetRolesByUserResponse>(response);
        }
        #endregion
    }
}
