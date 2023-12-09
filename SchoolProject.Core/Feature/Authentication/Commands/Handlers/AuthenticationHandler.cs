using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.Authentication.Commands.Models;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Feature.Authentication.Commands.Handlers
{
    public class AuthenticationHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtAuthenticationResponse>>
                                                         , IRequestHandler<RefreshTokenCommand, Response<JwtAuthenticationResponse>>
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
        public async Task<Response<JwtAuthenticationResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check If User Name Is Exist Or Not
            var user = await _userManager.FindByNameAsync(request.UserName);

            //If User Name Is Not Exist Return Not Found
            if (user == null) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.UserNameIsNotExist]);

            //Try To Sign In
            var signInResponse = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //If Failed Return Password Is Wrong 
            if (!signInResponse.IsCompletedSuccessfully) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.PasswordOrUserNameNotCorrect]);

            //Generate Token
            var response = await _authenticationService.GetJWTToken(user);

            //Return Token 
            return Success(response);
        }

        public async Task<Response<JwtAuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //Read Token To Get Cliams
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
            switch (userIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.AlgorithmIsWrong]);
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.TokenIsNotExpired]);
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.RefreshTokenIsNotFound]);
                case ("RefreshTokenIsNotExpired", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.RefreshTokenIsNotExpired]);
            }
            //Get User
            var (userId, expireDate) = userIdAndExpireDate;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound<JwtAuthenticationResponse>(_stringLocalizer[SharedResourseKey.UserNotFound]);
            var response = await _authenticationService.GetRefreshToken(user, expireDate, request.RefreshToken);
            return Success(response);
        }


        #endregion
    }
}
