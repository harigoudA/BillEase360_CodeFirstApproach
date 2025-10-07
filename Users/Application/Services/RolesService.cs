using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BillEase360_CodeFirstApproach.Users.Application.Services
{
    public class RolesService
    {
        public readonly IUserRolerepository _userRolerepository;

        public RolesService(IUserRolerepository userRolerepository)
        {
            _userRolerepository = userRolerepository;
        }

        public async Task<Role> CraeteRole(CreateRolesDto dto ,Guid id)
        {
            var role = new Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description,
                IsActive = dto.IsActive,
                CreatedBy = id,
                ModifiedBy = id,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedAt= DateTime.Now
            };

             await _userRolerepository.AddAsync(role);

            return role;


        }

        public async Task<IEnumerable<RoleResponseDto>> GetAllRolesAsync()
        {
            var roles=await _userRolerepository.GetAllAsync();

            return roles.Select(r => new RoleResponseDto
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                Description = r.Description
            });
        }

        public async  Task<bool> ActiavteRoleAsync(Guid userId,Guid currentUserId)
        {
            var roles= await _userRolerepository.GetByIdAsync(userId);
            if (roles == null)
            {
                throw new ArgumentException("User not found");
            }
            roles.IsActive=true;
            roles.ModifiedBy=currentUserId;
            roles.ModifiedDate=DateTime.Now;

            await _userRolerepository.UpdateAsync(roles);

            return true;


        }


        public async Task<bool> DeactiavteRoleAsync(Guid userId, Guid currentUserId)
        {
            var roles = await _userRolerepository.GetByIdAsync(userId);
            if (roles == null)
            {
                throw new ArgumentException("User not found");
            }
            roles.IsActive = false;
            roles.ModifiedBy = currentUserId;
            roles.ModifiedDate = DateTime.Now;

            await _userRolerepository.UpdateAsync(roles);

            return true;


        }

        public async Task<RoleResponseDto> UpdateRoleAsync(Guid roleid,CreateRolesDto role,Guid id)
        {
            var roles = await _userRolerepository.GetByIdAsync(roleid);
            if (role == null)
            {
                throw new ArgumentException("Role doesn't found");
            }
            roles.RoleName = role.RoleName;
            roles.Description = role.Description;
            roles.IsActive = role.IsActive;
            roles.ModifiedBy=roleid;
            roles.ModifiedDate = DateTime.Now;

            await _userRolerepository.UpdateAsync(roles);
            return new RoleResponseDto{
                RoleId = roleid,
                RoleName= role.RoleName,
                Description= role.Description,
                
            };

        }
    }
}
