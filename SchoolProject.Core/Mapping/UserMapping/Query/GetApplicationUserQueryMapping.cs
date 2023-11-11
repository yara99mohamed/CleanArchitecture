using SchoolProject.Core.Feature.ApplicationUser.Queries.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.UserMapping
{
    public partial class UserProfile
    {
        public void GetApplicationUserQueryMapping()
        {
            CreateMap<User, GetApplicationUserResponse>();
        }
    }
}
