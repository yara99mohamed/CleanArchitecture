using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Commands.Requests;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommandRequest, Response<string>>
                                                        , IRequestHandler<EditStudentCommandRequest, Response<string>>
                                                        , IRequestHandler<DeleteStudentCommandRequest, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region Constractors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResourse> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handle Functions

        public async Task<Response<string>> Handle(AddStudentCommandRequest request, CancellationToken cancellationToken)
        {
            // mapper between student and request
            var studentMapper = _mapper.Map<Student>(request);

            // add 
            var student = await _studentService.AddAsync(studentMapper);

            // check condition
            if (student == "Success") return Created("Add Successfully");
            else return BadRequest<string>("Bad Request");
        }

        public async Task<Response<string>> Handle(EditStudentCommandRequest request, CancellationToken cancellationToken)
        {
            // check if id exist or not
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null) return NotFound<string>("Student Is Not Found");
            // mapper between student and reques t
            var studentMapper = _mapper.Map(request, student);
            var response = await _studentService.EditAsync(studentMapper);
            if (response == "Success") return Success($"Edit Successfully {studentMapper}");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommandRequest request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null) return NotFound<string>("Student Is Not Found");

            var response = await _studentService.DeleteAsync(student);
            if (response == "Success") return Deleted<string>("Delete Successfully");
            else return BadRequest<string>();
        }
        #endregion
    }
}
