using BillEase360_CodeFirstApproach.Products.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Invoice.Domain.Entities
{
    public class InvoiceLineItems
    {
        [Key]
        public Guid LineItemID { get; set; }

        public Guid InvoiceID { get; set; }


        public Guid ProductID { get; set; }


        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }

        public decimal TaxableValue { get; set; }

        public decimal GSTRate { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
