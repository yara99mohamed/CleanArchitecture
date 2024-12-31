using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Mapping.RoleMapping
{
    public partial class RoleProfile
    {

        public void UpdateRolesByUserCommandMapping()
        {
            CreateMap<UpdateRolesUserCommand, RolesByUserRequest>();
        }

        public void UpdateClaimsByUserCommandMapping()
        {
            CreateMap<UpdateClaimsUserCommand, EditClaimsByUserRequest>();
        }
    }
}
