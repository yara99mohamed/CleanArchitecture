namespace SchoolProject.Core.Feature.Authorization.Queries.Responses
{
    public class GetRolesByUserResponse
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
}
