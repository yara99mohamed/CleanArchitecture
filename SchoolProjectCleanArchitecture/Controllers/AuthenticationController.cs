using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.Feature.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        #region Controllers  
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> create([FromForm] SignInCommand request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpGet(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }
        #endregion
    }
}
