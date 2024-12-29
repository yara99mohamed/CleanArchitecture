using SchoolProject.Core.Feature.Authorization.Commands.Models;
using SchoolProject.Data.ViewData;

namespace SchoolProject.Core.Mapping.RoleMapping
{
    public partial class RoleProfile
    {

        public void UpdateRolesByUserCommandMapping()
        {
            CreateMap<UpdateRolesUserCommand, RolesByUserViewData>();
        }
    }
}
