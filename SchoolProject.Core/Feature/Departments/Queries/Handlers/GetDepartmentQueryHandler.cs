using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Departments.Queries.Models;
using SchoolProject.Core.Feature.Departments.Queries.Responses;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Core.wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Feature.Departments.Queries.Handlers
{
    public class GetDepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region Constructors
        public GetDepartmentQueryHandler(IDepartmentService departmentService, IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResourse> stringLocalizer) : base(stringLocalizer)
        {
            _departmentService = departmentService;
            _studentService = studentService;
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
        }

        public async Task<Response<GetDepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            // service get by id include sub , student , instractor
            var department = await _departmentService.GetDepartmentWithIncludeByIdAsync(request.Id);

            // check  Is not exist return not found
            if (department == null) return NotFound<GetDepartmentResponse>(_stringLocalizer[SharedResourseKey.NotFount]);

            //mapping 
            var departmentMapping = _mapper.Map<GetDepartmentResponse>(department);


            // paginate student
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQueryable = _studentService.FilterStudentByDepartmentPaginatedQueryable(department.DID);
            var paginatedList = await studentQueryable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            departmentMapping.StudentList = paginatedList;
            return Success(departmentMapping);
        }
        #endregion

        #region Handle Functions 
        #endregion

    }
}
