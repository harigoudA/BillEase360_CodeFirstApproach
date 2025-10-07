using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;

namespace BillEase360_CodeFirstApproach.Users.Application.Interfaces
{
    public interface IUserRoleIdRepository
    {
        Task AddAsync(UserRole userRole);

        Task<IEnumerable<UserRole>> GetAllAsync();

        Task<UserRole?> GetIdAsync(Guid id);

        Task UpdateAsync(UserRole userRole);


    }
}
