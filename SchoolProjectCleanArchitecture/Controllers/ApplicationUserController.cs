using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        #region Controllers
        //[HttpGet(Router.UserRouting.List)]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var resonse = await _mediator.Send(new  ());
        //    return Ok(resonse);
        //}

        [HttpGet(Router.ApplicationUserRouting.Paginate)]
        public async Task<IActionResult> GetPaginatedUsers([FromQuery] GetApplicationUserPaginatedListQuery query)
        {
            var resonse = await _mediator.Send(query);
            return Ok(resonse);
        }

        [HttpGet(Router.ApplicationUserRouting.GetById)]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new GetApplicationUserByIdQuery(id));
            return NewResult(resonse);
        }

        [HttpPost(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> create([FromBody] AddUserCommand request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpPut(Router.ApplicationUserRouting.Update)]
        public async Task<IActionResult> Edit([FromBody] UpdateApplicationUserCommand request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpDelete(Router.ApplicationUserRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new DeleteApplicationUserCommand(id));
            return NewResult(resonse);
        }
        #endregion
    }
}

