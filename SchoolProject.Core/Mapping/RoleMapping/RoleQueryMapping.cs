using SchoolProject.Core.Feature.Authorization.Queries.Responses;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.RoleMapping
{
    public partial class RoleProfile
    {
        public void GetRoleQueryMapping()
        {
            CreateMap<Role, GetRoleResponse>();
        }
    }
}
