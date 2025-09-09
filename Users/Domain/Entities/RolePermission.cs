namespace BillEase360_CodeFirstApproach.Users.Domain.Entities
{
    public class RolePermission
    {
        public Guid RolePermissionId { get; set; } = Guid.NewGuid();

        // Foreign Keys
        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }

        // Navigation Properties
        public Role Role { get; set; }
        public Permission Permission { get; set; }

        // Audit Fields
        public bool IsActive { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
