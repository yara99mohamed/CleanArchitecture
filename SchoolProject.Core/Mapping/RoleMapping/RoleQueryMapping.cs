using SchoolProject.Core.Feature.Authorization.Queries.Responses;
using SchoolProject.Data.ViewData;

namespace SchoolProject.Core.Mapping.RoleMapping
{
    public partial class RoleProfile
    {
        public void GetRoleQueryMapping()
        {
            CreateMap<Role, GetRoleResponse>();
        }

        public void GetRolesByUserQueryMapping()
        {
            CreateMap<GetRolesByUserViewData, GetRolesByUserResponse>();
            CreateMap<UserRolesViewData, Role>();
        }
    }
}
