using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.ViewData;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authorization.Commands.Handlers
{
    public class RolesUserCommandHandler : ResponseHandler, IRequestHandler<UpdateRolesUserCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constractors
        public RolesUserCommandHandler(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationService authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(UpdateRolesUserCommand request, CancellationToken cancellationToken)
        {
            var requestMapper = _mapper.Map<RolesByUserViewData>(request);
            var result = await _authorizationService.UpdateRolesByUserId(requestMapper);
            if (result == "NotFound") return NotFound<string>("User Is Not Exsit");
            else if (result == "Success") return Success<string>("Roles Added For User Successfuly");
            else return BadRequest<string>(result);
        }
        #endregion
    }
}
