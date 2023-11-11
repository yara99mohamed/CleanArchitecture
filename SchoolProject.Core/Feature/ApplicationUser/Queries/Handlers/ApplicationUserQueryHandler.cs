using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Models;
using SchoolProject.Core.Feature.ApplicationUser.Queries.Responses;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Core.wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Handlers
{
    public class ApplicationUserQueryHandler : ResponseHandler, IRequestHandler<GetApplicationUserPaginatedListQuery, PaginatedResult<GetApplicationUserResponse>>
                                                              , IRequestHandler<GetApplicationUserByIdQuery, Response<GetApplicationUserResponse>>
    {
        #region Fields 
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constractors
        public ApplicationUserQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<PaginatedResult<GetApplicationUserResponse>> Handle(GetApplicationUserPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var PaginatedResultMapper = await _mapper.ProjectTo<GetApplicationUserResponse>(users)
                                                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedResultMapper;
        }

        public async Task<Response<GetApplicationUserResponse>> Handle(GetApplicationUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetApplicationUserResponse>(_stringLocalizer[SharedResourseKey.NotFount]);
            var userMapper = _mapper.Map<GetApplicationUserResponse>(user);
            return Success(userMapper);
        }
        #endregion

        #region
        #endregion
    }
}
