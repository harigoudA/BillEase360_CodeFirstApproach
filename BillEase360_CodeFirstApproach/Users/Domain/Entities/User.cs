using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Users.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required, MaxLength(70)]
        public string UserName { get; set; } = "";

        [MaxLength(255)]
        public string PasswordHash { get; set; } = "";

        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; } = "";

        [MaxLength(50)]
        public string LastName { get; set; } = "";

        public bool Status { get; set; }

        public bool IsActive { get; set; } = true;

        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public DateTime? LastLoginAt { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
