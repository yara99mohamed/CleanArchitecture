using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.Queries.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler, IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constractors 
        public AuthenticationQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        #endregion 

        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ValidateToken(request.AccessToken);
            if (response == "NotExpired")
                return Success(response);

            return Unauthorized<string>(_stringLocalizer[SharedResourseKey.TokenIsExpired]);
        }
    }
}
