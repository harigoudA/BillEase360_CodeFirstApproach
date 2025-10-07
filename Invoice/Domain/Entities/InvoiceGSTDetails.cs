using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Invoice.Domain.Entities
{
    public class InvoiceGSTDetails
    {
        [Key]
        public Guid GSTDetailID { get; set; }

        public Guid InvoiceID { get; set; }

        public Invoices Invoices { get; set; }

        public string GSTType { get; set; }

        public decimal Rate { get; set; }

        public decimal TaxAmount { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
