using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillEase360_CodeFirstApproach.Users.UI
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController:ControllerBase
    {
        private readonly PermissionService _permissionservice;
        public PermissionController(PermissionService permissionservice)
        {
            _permissionservice = permissionservice;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePermission([FromBody] AddPermissionsDto dto)
        {
            try
            {
                // Get current logged-in user GUID from JWT
                var currentUserId = GetCurrentUserId();
                var Permission = await _permissionservice.CreatePermission(dto, currentUserId);
                return Ok(Permission);
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
        public async Task<IActionResult> GetPermission()
        {
            try
            {
                var Permission = await _permissionservice.GetAllPermissionsAsync();

                return Ok(Permission);
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


        [HttpGet("PermissionId")]
        public async Task<IActionResult> GetPermisssionById(Guid id)
        {
            try
            {
                var perm = await _permissionservice.GetPermisssionByIdAsync(id);
                if (perm == null)
                {
                    return NotFound(new { Mesage = "Permission record not found" });
                }
                return Ok(perm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve permission", error = ex.Message });
            }

        }

        [HttpPatch("{id}/ActivateRole")]
        [Authorize]
        public async Task<IActionResult> ActivateRole(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _permissionservice.ActivateUserAsync(id, currentUserId);
                if (result)
                {
                    return Ok(new { message = "Permission activated sucessfully." });
                }

                return BadRequest(new { message = "Failed to activate permission." });
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new {message=ex.Message});
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "Permission activated failed", error=ex.Message});
            }
        }

        [HttpPatch("{id}/DeactivateRole")]
        [Authorize]
        public async Task<IActionResult> DeactivateRole(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _permissionservice.DeactivateUserAsync(id, currentUserId);
                if (result)
                {
                    return Ok(new { message = "Permission deactivated sucessfully." });
                }

                return BadRequest(new { message = "Failed to deactivate permission." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Permission deactivated failed", error = ex.Message });
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
