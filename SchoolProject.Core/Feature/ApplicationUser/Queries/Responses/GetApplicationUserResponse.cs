namespace SchoolProject.Core.Feature.ApplicationUser.Queries.Responses
{
    public class GetApplicationUserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
