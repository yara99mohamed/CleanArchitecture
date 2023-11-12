namespace SchoolProject.Data.Helper
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssure { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifeTime { get; set; }
        public bool ValidateIssureSigningKey { get; set; }
    }
}
