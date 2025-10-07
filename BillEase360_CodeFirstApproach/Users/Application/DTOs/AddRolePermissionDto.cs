using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class AddRolePermissionDto
    {
        public Guid PermissionID { get; set; }
        public Guid RoleID { get; set; }
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy  { get; set; }
    }
}
