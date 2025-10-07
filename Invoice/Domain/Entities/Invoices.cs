using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Invoice.Domain.Entities
{
    public class Invoices
    {
        [Key]
        public Guid InvoiceID {  get; set; }
        
        public Guid CustomerID { get; set; }

        public Guid UserID { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public Decimal TotalAmount { get; set; }

        public Decimal TaxableAmount { get; set; }

        public string Status { get; set; }

        public string PaymentStatus  { get; set; }

        public string InvoiceType { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }


    }
}
