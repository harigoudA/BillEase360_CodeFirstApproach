using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Products.Domain.Entities
{
    public class Categories
    {
        [Key]
        public Guid CategoryId { get; set; }
        [Required, StringLength(100)]
        public string CategoryName { get; set; } = "";

        [MaxLength(255)]
        public string Description { get; set; } = "";

        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
