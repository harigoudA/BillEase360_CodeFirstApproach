using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillEase360_CodeFirstApproach.Users.Application.Services
{
    public class PermissionService
    {
        private readonly IUserPermissionRepository _userPermissionRepository;

        public PermissionService(IUserPermissionRepository userPermissionRepository)
        {
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<Permission> CreatePermission(AddPermissionsDto dto, Guid id)
        {
            var _permission=new Permission{
                PermissionName=dto.PermissionName,
                Description=dto.Description,
                CreatedBy=id,
                ModifiedBy=id,
                IsActive=true,
            };

            await _userPermissionRepository.AddAsync(_permission);

            return _permission;

        }

        public async Task<IEnumerable<PermissionResposeDto>> GetAllPermissionsAsync()
        {
            var perm=await _userPermissionRepository.GetAllAsync();

            return perm.Select(p => new PermissionResposeDto
            {
                PermissionID=p.PermissionId,
                PermissionName=p.PermissionName,
                    Description=p.Description,
                    IsActive=p.IsActive
            });
        }

        public async Task<PermissionResposeDto?> GetPermisssionByIdAsync(Guid id)
        {
            var perm = await _userPermissionRepository.GetByIdAsync(id);

            return new PermissionResposeDto { 
            PermissionID=perm.PermissionId,
            PermissionName=perm.PermissionName,
            Description=perm.Description,
            IsActive=perm.IsActive
            };
        }

        public async Task<bool> ActivateUserAsync(Guid permissionId,Guid UserId)
        {
            var perm = await _userPermissionRepository.GetByIdAsync(permissionId);

            if(perm == null)
            {
                throw new ArgumentException("User not found");
            }
            
            perm.IsActive = true;
            perm.ModifiedBy = UserId;
            perm.ModifiedDate = DateTime.Now;

            await _userPermissionRepository.UpdatAdync(perm);
            return true;
        }

        public async Task<bool> DeactivateUserAsync(Guid permissionId, Guid UserId)
        {
            var perm = await _userPermissionRepository.GetByIdAsync(permissionId);

            if (perm == null)
            {
                throw new ArgumentException("User not found");
            }

            perm.IsActive = false;
            perm.ModifiedBy = UserId;
            perm.ModifiedDate = DateTime.Now;

            await _userPermissionRepository.UpdatAdync(perm);
            return true;
        }



    }
}
