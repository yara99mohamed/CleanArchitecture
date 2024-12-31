using SchoolProject.Core.Feature.Authorization.Queries.Responses;
using SchoolProject.Data.Requests;

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
            CreateMap<RolesByUserRequest, GetRolesByUserResponse>();
            CreateMap<RoleRequest, Role>().ReverseMap();
        }
    }
}
