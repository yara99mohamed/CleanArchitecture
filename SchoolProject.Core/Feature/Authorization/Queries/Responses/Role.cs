namespace SchoolProject.Core.Feature.Authorization.Queries.Responses
{
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool HasRole { get; set; }
    }
}
