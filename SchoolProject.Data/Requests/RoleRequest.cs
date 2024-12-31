namespace SchoolProject.Data.Requests
{
    public class RoleRequest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
