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
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRoleByIdQuery, Response<GetRoleResponse>>
                                                   , IRequestHandler<GetRoleListQuery, Response<List<GetRoleResponse>>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractors
        public RoleQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationService authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<GetRoleResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null) return NotFound<GetRoleResponse>("Role Is Not Exist");
            var result = _mapper.Map<GetRoleResponse>(role);
            return Success(result);
        }

        public async Task<Response<List<GetRoleResponse>>> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesAsync();
            var result = _mapper.Map<List<GetRoleResponse>>(roles);
            return Success(result);
        }
        #endregion
    }
}
