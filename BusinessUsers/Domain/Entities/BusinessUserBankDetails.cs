using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.BusinessUsers.Domain.Entities
{
    public class BusinessUserBankDetails
    {
        [Key]
        public Guid BankDetailID { get; set; }

       public Guid BusinessUserID { get; set; }

        public BusinessUser BusinessUsers {  get; set; }

        public string BankName  { get; set; }

        public string AccountNumber { get; set; }
       
        public string IFSCCode { get; set; }

        public string BranchName { get; set; }

        public bool IsActive    { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
