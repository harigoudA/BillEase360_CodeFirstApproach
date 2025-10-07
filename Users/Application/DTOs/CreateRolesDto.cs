namespace BillEase360_CodeFirstApproach.Users.Application.DTOs
{
    public class CreateRolesDto
    {
        public string RoleName { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsActive { get; set; } = true;

    }
}
