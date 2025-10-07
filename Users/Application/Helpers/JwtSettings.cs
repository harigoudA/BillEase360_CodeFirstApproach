namespace BillEase360_CodeFirstApproach.Users.Application.Helpers
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = "";
        public string Issuer { get; set; } = "";
        public string Audience { get; set; } = "";
        public int ExpiryInMinutes { get; set; } = 60;
        public int RefreshTokenExpiryInDays { get; set; } = 7;
    }
}
