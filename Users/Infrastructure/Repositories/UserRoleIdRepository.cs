using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BillEase360_CodeFirstApproach.Users.Infrastructure.Repositories
{
    public class UserRoleIdRepository:IUserRoleIdRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRoleIdRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(UserRole userRole)
        {
            _appDbContext.UserRoles.Add(userRole);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _appDbContext.UserRoles.ToListAsync();
        }

        public async Task<UserRole?> GetIdAsync(Guid id)
        {
            return await _appDbContext.UserRoles.FindAsync(id);
        }

        public async Task UpdateAsync(UserRole userRole)
        {
            _appDbContext.UserRoles.Update(userRole);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
