using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Students.Queries.Request;
using SchoolProject.Core.Feature.Students.Queries.Response;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Core.wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Feature.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>
                                                      , IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>
                                                      , IRequestHandler<GetStudentPaginatedListQueryRequest, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        #endregion

        #region Constractors
        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResourse> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion

        #region Handle Functions

        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetAllStudentServicesAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);
            var response = Success(studentListMapper);
            response.Meta = new { Count = studentListMapper.Count() };
            return response;
        }

        public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null) { return NotFound<GetStudentResponse>(_stringLocalizer[SharedResourseKey.NotFount]); }
            var response = _mapper.Map<GetStudentResponse>(student);
            return Success(response);
        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQueryRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudID, e.Localize(e.NameAr, e.NameEn), e.Address, e.Localize(e.Department.DNameAr, e.Department.DNameEn));
            //var querable = _studentService.GetAllStudentQueryablServices();//request.OrderBy,
            var filterQuery = _studentService.FilterStudentPaginatedQueryable(request.Search);
            var paginatedList = await filterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            var response = paginatedList;
            response.Meta = new { Count = paginatedList.Data.Count() };
            return response;
        }
        #endregion
    }
}
