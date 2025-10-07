using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace BillEase360_CodeFirstApproach.Users.UI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            try
            {
                // Get current logged-in user GUID from JWT
                var currentUserId = Guid.Parse(User.FindFirst("sub")?.Value ?? Guid.Empty.ToString());
                var user = await _userService.CreateUserAsync(dto, currentUserId);
                return Ok(user);
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
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to retrieve user", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] CreateUserDto dto)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var user = await _userService.UpdateUserAsync(id, dto, currentUserId);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "User update failed", error = ex.Message });
            }
        }

        [HttpPatch("{id}/deactivate")]
        [Authorize]
        public async Task<IActionResult> DeactivateUser(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _userService.DeactivateUserAsync(id, currentUserId);

                if (result)
                {
                    return Ok(new { message = "User deactivated successfully" });
                }

                return BadRequest(new { message = "Failed to deactivate user" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "User deactivation failed", error = ex.Message });
            }
        }

        [HttpPatch("{id}/activate")]
        [Authorize]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _userService.ActivateUserAsync(id, currentUserId);

                if (result)
                {
                    return Ok(new { message = "User activated successfully" });
                }

                return BadRequest(new { message = "Failed to activate user" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "User activation failed", error = ex.Message });
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var response = await _userService.LoginAsync(dto);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Login failed", error = ex.Message });
            }
        }


        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var result = await _userService.ChangePasswordAsync(currentUserId, changePasswordDto);

                if (result)
                {
                    return Ok(new { message = "Password changed successfully" });
                }

                return BadRequest(new { message = "Failed to change password" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Password change failed", error = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            // With JWT, logout is typically handled on the client side
            // by simply discarding the token
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var user = await _userService.GetUserByIdAsync(currentUserId);

                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }

                // Include additional claims from token
                var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);

                return Ok(new
                {
                    user,
                    claims = new
                    {
                        userId = claims.GetValueOrDefault("sub"),
                        email = claims.GetValueOrDefault("email"),
                        name = claims.GetValueOrDefault("name"),
                        username = claims.GetValueOrDefault("username")
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to get profile", error = ex.Message });
            }
        }


        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetUserDetails(Guid id)
        {
            var user = await _userService.GetUserWithRolesAndPermissionsAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
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
