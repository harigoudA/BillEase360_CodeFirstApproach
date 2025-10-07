using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using System.Security.Cryptography.Xml;

namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class UserDetails
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = "";

        public string Email { get; set; } = "";

        public string FullName { get; set; } = "";

        public bool IsActive { get; set; }

        public List<RoleResponseDto> Roles { get; set; } = new();

        public List<PermissionResposeDto> Permissions { get; set; } = new();


    }
}
