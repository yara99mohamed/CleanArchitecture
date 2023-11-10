using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Bases;
using SchoolProject.Core.Feature.Students.Commands.Requests;
using SchoolProject.Core.Feature.Students.Queries.Request;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class StudentsController : AppControllerBase
    {
        #region Controllers
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudens()
        {
            var resonse = await _mediator.Send(new GetStudentListQuery());
            return Ok(resonse);
        }

        [HttpGet(Router.StudentRouting.Paginate)]
        public async Task<IActionResult> GetPaginatedStudens([FromQuery] GetStudentPaginatedListQueryRequest query)
        {
            var resonse = await _mediator.Send(query);
            return Ok(resonse);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(resonse);
        }

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> create([FromBody] AddStudentCommandRequest request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommandRequest request)
        {
            var resonse = await _mediator.Send(request);
            return NewResult(resonse);
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var resonse = await _mediator.Send(new DeleteStudentCommandRequest(id));
            return NewResult(resonse);
        }
        #endregion
    }
}
