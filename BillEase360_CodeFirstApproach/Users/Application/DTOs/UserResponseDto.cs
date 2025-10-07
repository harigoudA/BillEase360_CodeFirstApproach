namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class UserResponseDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string FullName { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
