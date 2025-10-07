using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string? PhoneNumber { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        
    }
}
