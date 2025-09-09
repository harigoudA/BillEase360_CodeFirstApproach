namespace BillEase360_CodeFirstApproach.Users.Domain.Entities
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; } = Guid.NewGuid();

        // Foreign Keys
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Role Role { get; set; }

        // Audit Fields
        public bool IsActive { get; set; } = true;
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
