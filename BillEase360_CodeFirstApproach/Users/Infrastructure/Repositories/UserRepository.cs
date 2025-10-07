using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillEase360_CodeFirstApproach.Users.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        //public async Task DeleteAsync(Guid id)
        //{
        //    var user = await GetByIdAsync(id);
        //    if (user != null)
        //    {
        //        // Soft delete - just mark as inactive
        //        user.Status = false;
        //        user.IsActive = false;
        //        user.ModifiedDate = DateTime.UtcNow;
        //        await UpdateAsync(user);
        //    }
        //}

        // New methods for authentication
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower() && u.Status == true);
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName.ToLower() == userName.ToLower() && u.Status == true);
        }

        public async Task<UserDetails> GetUserFullDetailsAsync(Guid userId)
        {
            var user=await _context.Users
                .Include(u => u.UserRoles) 
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r=>r.RolePermissions)
                .ThenInclude(rp=>rp.Permission)
                .FirstOrDefaultAsync(u=>u.UserId == userId);

            if (user == null)
                return null;

            var respose = new UserDetails
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                IsActive = user.IsActive,
                Roles = user.UserRoles.Select(ur => new RoleResponseDto
                {
                    RoleId = ur.Role.RoleId,
                    RoleName = ur.Role.RoleName,

                }).ToList(),
                Permissions=user.UserRoles.SelectMany(ur=>ur.Role.RolePermissions)
                                .Select(rp=>new PermissionResposeDto
                                {
                                    PermissionID=rp.Permission.PermissionId,
                                    PermissionName=rp.Permission.PermissionName,
                                })
                                .DistinctBy(p=>p.PermissionID).ToList()

            };

            return respose;
        }

     }
}
