using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authentication.Commands.Handlers
{
    public class AuthenticationHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        #endregion

        #region Constractors 
        public AuthenticationHandler(IStringLocalizer<SharedResourse> stringLocalizer, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IAuthenticationService authenticationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _authenticationService = authenticationService;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        #endregion


        #region Handle Functions
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check If User Name Is Exist Or Not
            var user = await _userManager.FindByNameAsync(request.UserName);

            //If User Name Is Not Exist Return Not Found
            if (user == null) return BadRequest<string>(_stringLocalizer[SharedResourseKey.UserNameIsNotExist]);

            //Try To Sign In
            var signInResponse = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //If Failed Return Password Is Wrong
            if (!signInResponse.IsCompletedSuccessfully) return BadRequest<string>(_stringLocalizer[SharedResourseKey.PasswordOrUserNameNotCorrect]);

            //Generate Token
            var accessToken = await _authenticationService.GetJWTToken(user);
            //Return Token

            return Success(accessToken);
        }
        #endregion
    }
}
