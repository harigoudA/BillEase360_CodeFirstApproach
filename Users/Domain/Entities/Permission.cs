namespace BillEase360_CodeFirstApproach.Users.Domain.Entities
{
    public class Permission
    {
        public Guid PermissionId { get; set; } = Guid.NewGuid();
        public string PermissionName { get; set; }
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
