namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = "";
        public UserResponseDto User { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public string TokenType { get; set; } = "Bearer";
    }
}
