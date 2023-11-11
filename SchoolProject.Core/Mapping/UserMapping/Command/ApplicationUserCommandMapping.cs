using Microsoft.AspNetCore.Identity;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Models;
using SchoolProject.Core.Feature.ApplicationUser.Commands.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void AddApplicationUserCommandMapping()
        {
            CreateMap<AddUserCommand, User>();
            CreateMap<IdentityResult, AddUserResponse>();
        }
    }
}
