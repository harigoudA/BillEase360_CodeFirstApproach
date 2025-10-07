using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillEase360_CodeFirstApproach.Users.Infrastructure.Repositories
{
    public class RolesRepository:IUserRolerepository
    {
        private readonly AppDbContext _appDbContext;

        public RolesRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Role role)
        {
            _appDbContext.Roles.Add(role);
            await _appDbContext.SaveChangesAsync();
        }

        

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _appDbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Roles.FindAsync(id);
        }

        public async Task UpdateAsync(Role role)
        {
            _appDbContext.Roles.Update(role);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
