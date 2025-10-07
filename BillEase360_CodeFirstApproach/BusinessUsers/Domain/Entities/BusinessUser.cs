using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.BusinessUsers.Domain.Entities
{
    public class BusinessUser
    {
        [Key]
        public Guid BusinessUserID { get; set; }

        public string Name { get; set; }

        public string BusinessUserType { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string GSTIN { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
