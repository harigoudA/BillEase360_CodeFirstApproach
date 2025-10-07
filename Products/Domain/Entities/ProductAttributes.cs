using System.ComponentModel.DataAnnotations;

namespace BillEase360_CodeFirstApproach.Products.Domain.Entities
{
    public class ProductAttributes
    {
        [Key]
        public Guid AttributeID { get; set; }
        
        //Foeign key
        public Guid ProductID { get; set; }

        //Navigation properties
        public Product Product { get; set; }

        public string AttributeName { get; set; }

        public string AttributeValue { get; set; }

        public string AttributeType { get; set; }

        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        }
    }
