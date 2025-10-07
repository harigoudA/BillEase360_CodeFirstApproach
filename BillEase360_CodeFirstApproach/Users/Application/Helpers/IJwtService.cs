using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using System.Security.Claims;

namespace BillEase360_CodeFirstApproach.Users.Application.Helpers
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
        string? GetUserIdFromToken(string token);
    }
}
