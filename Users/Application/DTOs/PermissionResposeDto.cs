namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class PermissionResposeDto
    {
        public Guid PermissionID { get; set; }

        public string PermissionName { get; set; } = "";

        public string Description { get; set; } = "";

        public bool IsActive { get; set; }
    }
}
