namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class AddUserRolesDto
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public bool IsActive { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }
    }
}
