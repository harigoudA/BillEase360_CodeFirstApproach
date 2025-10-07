using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Products.Domain.Entities
{
    public class TaxSlabs
    {
        [Key]
        public Guid TaxSlabID {  get; set; }

        public String SlabName { get; set; } = "";

        public Decimal GST { get; set; }

        public DateOnly EffectiveFrom { get; set; }

        public DateOnly EffectiveTo {  get; set; }

        public bool IsActive { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
