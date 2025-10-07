using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillEase360_CodeFirstApproach.Users.UI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolesController:ControllerBase
    {
        private readonly UserRoleService _userRoleService;

        public UserRolesController(UserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRoles([FromBody] AddUserRolesDto dto)
        {
            try
            {
                // Get current logged-in user GUID from JWT
                var currentUserId = GetCurrentUserId();
                var userRoles = await _userRoleService.CreateUserRole(dto, currentUserId);

                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                // This will show us the exact error
                Console.WriteLine($"Exception: {ex}");
                Console.WriteLine($"Inner Exception: {ex.InnerException}");

                return BadRequest(new
                {
                    message = ex.Message,
                    innerMessage = ex.InnerException?.Message,
                    type = ex.GetType().Name
                });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserRoles()
        {
            try
            {
                var userRoles = await _userRoleService.GetAllUserRoleAsync();

                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                // This will show us the exact error
                Console.WriteLine($"Exception: {ex}");
                Console.WriteLine($"Inner Exception: {ex.InnerException}");

                return BadRequest(new
                {
                    message = ex.Message,
                    innerMessage = ex.InnerException?.Message,
                    type = ex.GetType().Name
                });
            }
        }


        [HttpGet("{id}/")]
        [Authorize]

        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var result = await _userRoleService.GetRoleByIdAsync(id);

            return Ok(result);
        }


        #region Helper Methods

        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid or missing user token");
            }
            return userId;
        }



        #endregion
    }
}
