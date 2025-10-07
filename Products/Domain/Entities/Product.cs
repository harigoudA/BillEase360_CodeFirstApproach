using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Products.Domain.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = "";

        // Foreign Keys
        public Guid CategoryID { get; set; }

        public Guid TaxSlabID { get; set; }

        // Navigation properties
        public Categories Category { get; set; }   // 👈 one product belongs to one category
        public TaxSlabs TaxSlab { get; set; }

        [StringLength(15)]

        public string HSN_SAC_Code { get; set; }

        public Decimal GST_Rate     { get; set; }

        public String Unit {  get; set; }

        public Decimal SalesPrice {get; set; }

        public Decimal PurchasePrice {get; set; }

        public string Description { get; set; }

        public Decimal StockQty     { get; set; }

        public Decimal ReorderLevel { get; set; }

        public bool IsComposition { get; set; }

        public bool IsActive {get; set; }

        public Guid? CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }= DateTime.Now;
        public DateTime ModifiedDate { get; set; }=DateTime.Now;


    }
}
