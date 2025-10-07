using BillEase360_CodeFirstApproach.Users.Domain.Entities;

namespace BillEase360_CodeFirstApproach.Users.Application.Interfaces
{
    public interface IUserRolerepository
    {
        Task AddAsync(Role role);
        //Task DeletAsync(Guid id);

        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role?> GetByIdAsync(Guid id);

        Task UpdateAsync(Role role);
    }
}
