using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;

namespace BillEase360_CodeFirstApproach.Users.Application.Services
{
    public class UserRoleService
    {
        private readonly IUserRoleIdRepository _roleIdRepository;

        public UserRoleService(IUserRoleIdRepository roleIdRepository)
        {
            _roleIdRepository = roleIdRepository;
        }

        public async Task<UserRole> CreateUserRole(AddUserRolesDto dto,Guid id)
        {
            var userRoles = new UserRole { 
            UserId=dto.UserID,
            RoleId=dto.RoleID,
            IsActive=dto.IsActive,
            CreatedBy=id,
            ModifiedBy=id
            };

            await _roleIdRepository.AddAsync(userRoles);

            return userRoles;
        }

        public async Task<IEnumerable<ResponseUserRoleDto?>> GetAllUserRoleAsync()
        {
            var userRoles=await _roleIdRepository.GetAllAsync();

            return userRoles.Select(ur => new ResponseUserRoleDto { 
            UserID=ur.UserId,
            RoleID=ur.RoleId,
            IsActive=ur.IsActive
            });

        }

        public async Task<ResponseUserRoleDto> GetRoleByIdAsync(Guid id)
        {
            var roles=await _roleIdRepository.GetIdAsync(id);
            if (roles == null)
                return null;

            return new ResponseUserRoleDto
            {
                UserRoleID=roles.UserRoleId,
                UserID = roles.UserId,
                RoleID = roles.RoleId,
                IsActive = roles.IsActive
            };

        }
    }
}
