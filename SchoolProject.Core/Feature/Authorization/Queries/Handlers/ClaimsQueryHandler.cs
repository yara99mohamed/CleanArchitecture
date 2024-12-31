using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authorization.Queries.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<GetClaimsByUserQuery, Response<ClaimsByUserResult>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constractors 
        public ClaimsQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        #endregion

        #region Functions 
        public async Task<Response<ClaimsByUserResult>> Handle(GetClaimsByUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.GetClaimsByUserId(request.UserId);
            if (result.Item1 == "NotFound") return NotFound<ClaimsByUserResult>("User Is Not Exist");
            return Success<ClaimsByUserResult>(result.Item2);
        }
        #endregion
    }
}
