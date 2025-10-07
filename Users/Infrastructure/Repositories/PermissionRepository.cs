using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillEase360_CodeFirstApproach.Users.Infrastructure.Repositories
{
    public class PermissionRepository:IUserPermissionRepository
    {
        private readonly AppDbContext _appDbContext;

        public PermissionRepository(AppDbContext appDbContext) 
        { 
                _appDbContext= appDbContext;
        }

        public async Task AddAsync(Permission permission)
        {
            _appDbContext.Permissions.Add(permission);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await _appDbContext.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Permissions.FindAsync(id);
        }

        public async Task UpdatAdync(Permission perm)
        {
            _appDbContext.Permissions.Update(perm);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
