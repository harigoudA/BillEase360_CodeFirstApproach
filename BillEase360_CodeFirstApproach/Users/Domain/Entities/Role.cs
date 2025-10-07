using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Users.Domain.Entities
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }

        [MaxLength(50)]
        public string RoleName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public DateTime? CreatedAt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
