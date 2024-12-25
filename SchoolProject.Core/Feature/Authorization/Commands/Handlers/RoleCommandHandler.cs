using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly RoleManager<Role> _roleManager;
        #endregion

        #region Constractors
        public RoleCommandHandler(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationService authorizationService, RoleManager<Role> roleManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _roleManager = roleManager;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success") return Success<string>("Role Add Successfuly");
            else return BadRequest<string>(result);
        }
        #endregion
    }
}
