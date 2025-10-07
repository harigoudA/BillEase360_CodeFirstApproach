namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class AddPermissionsDto
    {

        public string PermissionName { get; set; } = "";

        public string Description { get; set; } = "";
        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public bool IsActive { get; set; }
    }
}
