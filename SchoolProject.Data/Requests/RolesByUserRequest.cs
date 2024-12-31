namespace SchoolProject.Data.Requests
{
    public class RolesByUserRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public ICollection<RoleRequest>? Roles { get; set; }
    }
}
