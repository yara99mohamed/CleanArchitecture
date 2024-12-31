using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Core.Feature.Authorization.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpGet(Router.AuthorizationRouting.GetById)]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new GetRoleByIdQuery(id));
            return NewResult(resonse);
        }

        [HttpGet(Router.AuthorizationRouting.List)]
        public async Task<IActionResult> GetRoles()
        {
            var resonse = await _mediator.Send(new GetRoleListQuery());
            return NewResult(resonse);
        }

        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddRoleCommand request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpPost(Router.AuthorizationRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditRoleCommand request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new DeleteRoleCommand(id));
            return NewResult(resonse);
        }

        [HttpGet(Router.AuthorizationRouting.RolesByUser)]
        public async Task<IActionResult> GetRolesbyUserId([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new GetRolesByUserQuery(id));
            return NewResult(resonse);
        }

        [HttpPut(Router.AuthorizationRouting.UpdateRolesByUser)]
        public async Task<IActionResult> UpdateRolesbyUserId([FromBody] UpdateRolesUserCommand command)
        {
            var resonse = await _mediator.Send(command);
            return NewResult(resonse);
        }

        [HttpGet(Router.AuthorizationRouting.ClaimsByUser)]
        public async Task<IActionResult> GetClaimsbyUserId([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new GetClaimsByUserQuery() { UserId = id });
            return NewResult(resonse);
        }

        [HttpPut(Router.AuthorizationRouting.UpdateClaimsByUser)]
        public async Task<IActionResult> UpdateClaimsbyUserId([FromBody] UpdateClaimsUserCommand command)
        {
            var resonse = await _mediator.Send(command);
            return NewResult(resonse);
        }
    }
}
