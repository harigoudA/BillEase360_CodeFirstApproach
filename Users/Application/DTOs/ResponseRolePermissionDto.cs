namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class ResponseRolePermissionDto
    {
        public Guid RolePermissionID { get; set; }
        public Guid PermissionID { get; set; }
        public Guid RoleID { get; set; }
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}
