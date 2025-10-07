namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class ResponseUserRoleDto
    {
        public Guid UserRoleID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public bool IsActive { get; set; }
    }
}
