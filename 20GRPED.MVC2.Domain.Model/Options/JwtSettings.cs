namespace _20GRPED.MVC2.Domain.Model.Options
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
    }
}
