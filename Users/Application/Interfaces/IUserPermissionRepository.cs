using BillEase360_CodeFirstApproach.Users.Domain.Entities;

namespace BillEase360_CodeFirstApproach.Users.Application.Interfaces
{
    public interface IUserPermissionRepository
    {
        Task AddAsync(Permission permission);

        Task<IEnumerable<Permission>> GetAllAsync();

        Task<Permission?> GetByIdAsync(Guid id);

        Task UpdatAdync(Permission perm);
    }
}
