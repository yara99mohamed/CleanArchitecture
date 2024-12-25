using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> create([FromBody] AddRoleCommand request)
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
    }
}
