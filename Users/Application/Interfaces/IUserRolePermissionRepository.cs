using BillEase360_CodeFirstApproach.Users.Domain.Entities;

namespace BillEase360_CodeFirstApproach.Users.Application.Interfaces
{
    public interface IUserRolePermissionRepository
    {
        Task AddAsync(RolePermission rolePermission);

        Task<IEnumerable<RolePermission>> GetAllAsync();

        Task<RolePermission?> GetByIdAync(Guid id);
    }
}
