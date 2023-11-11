using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Responses;
using SchoolProject.Core.SharedResourses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Feature.ApplicationUser.Commands.Handlers
{
    public class AddUserHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<AddUserResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constractors 
        public AddUserHandler(IStringLocalizer<SharedResourse> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion


        #region Handle Functions
        public async Task<Response<AddUserResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            // if email is exist return exist email
            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null) return BadRequest<AddUserResponse>(_stringLocalizer[SharedResourseKey.EmailIsExist]);

            // if user name is exist return exist user name
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userByUserName != null) return BadRequest<AddUserResponse>(_stringLocalizer[SharedResourseKey.UserNameIsExist]);

            //Mapping from AddUserCommand to User
            var userMapper = _mapper.Map<User>(request);

            // Create User
            var response = await _userManager.CreateAsync(userMapper, request.Password);

            //if happen failed return massage
            if (!response.Succeeded) return BadRequest<AddUserResponse>(response.Errors.FirstOrDefault().Description);

            //mapping from user to AddUserResponse
            var responseMapper = _mapper.Map<AddUserResponse>(response);

            //User Add Successfully
            return Success(responseMapper, _stringLocalizer[SharedResourseKey.AddUserSuccessfully]);
        }
        #endregion 
    }
}
