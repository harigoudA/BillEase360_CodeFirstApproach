using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;

namespace BillEase360_CodeFirstApproach.Users.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);

        Task UpdateAsync(User user);
        //Task DeleteAsync(Guid id);


        // New methods for authentication
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);

        Task<UserDetails> GetUserFullDetailsAsync(Guid userId);
    }
}
