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
    public class ApplicationUserHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<AddUserResponse>>
                                                   , IRequestHandler<UpdateApplicationUserCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constractors 
        public ApplicationUserHandler(IStringLocalizer<SharedResourse> stringLocalizer, IMapper mapper, UserManager<User> userManager) : base(stringLocalizer)
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

            //If Happen Failed Return Massage
            if (!response.Succeeded) return BadRequest<AddUserResponse>(response.Errors.FirstOrDefault().Description);

            //mapping from user to AddUserResponse
            var responseMapper = _mapper.Map<AddUserResponse>(response);

            //User Add Successfully
            return Success(responseMapper, _stringLocalizer[SharedResourseKey.AddUserSuccessfully]);
        }

        public async Task<Response<string>> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            // check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            //if user not found
            if (user == null) return BadRequest<string>(_stringLocalizer[SharedResourseKey.NotFount]);

            //Mapping From  UpdateApplicationUserCommand To User
            var userMapper = _mapper.Map(request, user);

            //Updated User
            var response = await _userManager.UpdateAsync(userMapper);

            //If Happen Failed Return Massage
            if (!response.Succeeded) return BadRequest<string>(response.Errors.FirstOrDefault().Description);

            //User Add Successfully
            return Success<string>(_stringLocalizer[SharedResourseKey.Successed]);
        }
        #endregion
    }
}
