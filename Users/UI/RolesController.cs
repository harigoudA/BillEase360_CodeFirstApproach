using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillEase360_CodeFirstApproach.Users.UI
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController:ControllerBase
    {

        private readonly RolesService _rolesService;
        
        public RolesController(RolesService rolesService)
        {
            _rolesService=rolesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles([FromBody] CreateRolesDto dto)
        {
            try
            {
                // Get current logged-in user GUID from JWT
                var currentUserId = GetCurrentUserId();
                var Roles = await _rolesService.CraeteRole(dto, currentUserId);
                return Ok(Roles);
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
        public async Task<IActionResult> GetRoles()
        {
            try
            {

                var roles = await _rolesService.GetAllRolesAsync();

                return Ok(roles);
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

        [HttpPatch("{id}/ActivateRole")]
        [Authorize]
        public async Task<IActionResult> ActivateRole(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var roles = await _rolesService.ActiavteRoleAsync(id, currentUserId);

                if (roles)
                {
                    return Ok(new { message = "Role activated sucessfully." });
                }

                return BadRequest(new { message = "Failed to activate role." });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "User activation failed", error = ex.Message  });
            }
        }

        [HttpPatch("{id}/DeactivateRole")]
        [Authorize]
        public async Task<IActionResult> DeactivateRole(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var roles = await _rolesService.DeactiavteRoleAsync(id, currentUserId);

                if (roles)
                {
                    return Ok(new { message = "Role Deactivated sucessfully." });
                }

                return BadRequest(new { message = "Failed to Deactivate role." });

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "User deactivation failed", error = ex.Message });
            }
        }

        [HttpPut("{id}/UpdateRole")]
        [Authorize]
        public async Task<IActionResult> UpdareRole(Guid id, [FromBody] CreateRolesDto dto)
        {
            try
            {
                var currentUserid = GetCurrentUserId();
                var role = await _rolesService.UpdateRoleAsync(id, dto, currentUserid);
                return Ok(role);
            }
            catch (ArgumentException ex)
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
