using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Helpers;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BillEase360_CodeFirstApproach.Users.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly JwtSettings _jwtSettings;
        public UserService(IUserRepository userRepository, IJwtService jwtService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;
        }

        #region User Management Methods

        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto dto,Guid currentUsrId)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = PasswordHelper.HashPassword(dto.Password), // later hash it
                PhoneNumber = dto.PhoneNumber,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreatedBy= currentUsrId,
                ModifiedBy= currentUsrId,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IsActive = true,
                Status=true
            };

            await _userRepository.AddAsync(user);

            return new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}",
                IsActive = user.IsActive
            };
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                FullName = $"{u.FirstName} {u.LastName}",
                IsActive = u.IsActive
            });
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user == null ? null : MapToUserResponseDto(user);
        }

        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email.ToLowerInvariant());
            return user == null ? null : MapToUserResponseDto(user);
        }

        public async Task<UserResponseDto> UpdateUserAsync(Guid userId, CreateUserDto dto, Guid currentUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            // Update user properties (don't update password here)
            user.UserName = dto.UserName;
            user.Email = dto.Email.ToLowerInvariant();
            user.PhoneNumber = dto.PhoneNumber;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.ModifiedBy = currentUserId;
            user.ModifiedDate = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return MapToUserResponseDto(user);
        }

        public async Task<bool> DeactivateUserAsync(Guid userId, Guid currentUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.IsActive = false;
            user.ModifiedBy = currentUserId;
            user.ModifiedDate = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ActivateUserAsync(Guid userId, Guid currentUserId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.IsActive = true;
            user.ModifiedBy = currentUserId;
            user.ModifiedDate = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
            return true;
        }

#endregion


        #region Authentication Methods

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new ArgumentException("Email and password are required");
            }

            // Find user by email
            var user = await _userRepository.GetByEmailAsync(loginDto.Email.ToLowerInvariant());

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Verify password
            if (!PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            // Check if user is active
            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Account is deactivated");
            }

            // Update last login
            user.LastLoginAt = DateTime.UtcNow;
            await _userRepository.UpdateAsync(user);

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);
            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

            return new LoginResponseDto
            {
                Token = token,
                User = MapToUserResponseDto(user),
                ExpiresAt = expiresAt,
                TokenType = "Bearer"
            };
        }

        public async Task<UserResponseDto> RegisterAsync(CreateUserDto createUserDto)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(createUserDto.Email) ||
                string.IsNullOrWhiteSpace(createUserDto.Password) ||
                string.IsNullOrWhiteSpace(createUserDto.UserName))
            {
                throw new ArgumentException("Email, username, and password are required");
            }

            // Check if user already exists by email
            var existingUserByEmail = await _userRepository.GetByEmailAsync(createUserDto.Email.ToLowerInvariant());
            if (existingUserByEmail != null)
            {
                throw new ArgumentException("User with this email already exists");
            }

            // Check if username already exists
            var existingUserByUsername = await _userRepository.GetByUserNameAsync(createUserDto.UserName.ToLowerInvariant());
            if (existingUserByUsername != null)
            {
                throw new ArgumentException("Username already exists");
            }

            // Create new user (for registration, CreatedBy is system/empty)
            return await CreateUserAsync(createUserDto, Guid.Empty);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
        {
            if (string.IsNullOrWhiteSpace(changePasswordDto.CurrentPassword) ||
                string.IsNullOrWhiteSpace(changePasswordDto.NewPassword))
            {
                throw new ArgumentException("Current and new passwords are required");
            }

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            // Verify current password
            if (!PasswordHelper.VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Current password is incorrect");
            }

            // Update password
            user.PasswordHash = PasswordHelper.HashPassword(changePasswordDto.NewPassword);
            user.ModifiedDate = DateTime.UtcNow;
            user.ModifiedBy = userId; // User is changing their own password

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var user = await _userRepository.GetByEmailAsync(email.ToLowerInvariant());
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            user.PasswordHash = PasswordHelper.HashPassword(newPassword);
            user.ModifiedDate = DateTime.UtcNow;
            // ModifiedBy could be system user for password reset

            await _userRepository.UpdateAsync(user);
            return true;
        }


        public string TestTokenGeneration(User user)
        {
            Console.WriteLine("TEST: Generating token for dummy user");
            return _jwtService.GenerateToken(user);
        }


        public async Task<UserDetails> GetUserWithRolesAndPermissionsAsync(Guid userId)
        {
            return await _userRepository.GetUserFullDetailsAsync(userId);
        }


        #endregion


        #region Private Helper Methods

        private static UserResponseDto MapToUserResponseDto(User user)
        {
            return new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName}".Trim(),
                IsActive = user.IsActive
            };
        }

        #endregion

    }
}
