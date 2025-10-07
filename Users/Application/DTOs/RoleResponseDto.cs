namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class RoleResponseDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = "";

        public string Description { get; set; }= "";
    }
}
