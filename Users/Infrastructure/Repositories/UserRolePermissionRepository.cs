using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillEase360_CodeFirstApproach.Users.Infrastructure.Repositories
{
    public class UserRolePermissionRepository:IUserRolePermissionRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRolePermissionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(RolePermission rolePermission)
        {
           _appDbContext.RolePermissions.Add(rolePermission);
          await  _appDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<RolePermission>> GetAllAsync()
        {
            return await _appDbContext.RolePermissions.ToListAsync();
        }

        public async Task<RolePermission?> GetByIdAync(Guid id)
        {
            return await _appDbContext.RolePermissions.FindAsync(id);
        }
    }
}
