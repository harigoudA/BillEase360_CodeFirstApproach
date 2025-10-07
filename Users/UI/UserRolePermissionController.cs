using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillEase360_CodeFirstApproach.Users.UI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRolePermissionController:ControllerBase
    {
        private readonly RolePermissionService _rolePermissionService;

        public UserRolePermissionController(RolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRolePermission([FromBody] AddRolePermissionDto rolePermissionDto)
        {
            try
            {
                // Get current logged-in user GUID from JWT
                var currentUserId = GetCurrentUserId();
                var rolePermission = await _rolePermissionService.CreateRolePermission(rolePermissionDto, currentUserId);
                return Ok(rolePermission);

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
        public async Task<IActionResult> GetRolePermission()
        {
            try
            {
                var rolePermission = await _rolePermissionService.GetAllRolePermissions();
                return Ok(rolePermission);
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
