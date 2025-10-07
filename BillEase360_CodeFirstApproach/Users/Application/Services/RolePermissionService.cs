using BillEase360_CodeFirstApproach.Users.Application.DTOs;
using BillEase360_CodeFirstApproach.Users.Application.Interfaces;
using BillEase360_CodeFirstApproach.Users.Domain.Entities;

namespace BillEase360_CodeFirstApproach.Users.Application.Services
{
    public class RolePermissionService
    {
        private readonly IUserRolePermissionRepository _userRolePermissionrepository;

        public RolePermissionService(IUserRolePermissionRepository userRolePermissionrepository)
        {
            _userRolePermissionrepository = userRolePermissionrepository;
        }

        public async Task<RolePermission> CreateRolePermission(AddRolePermissionDto dto,Guid id)
        {
            var rolePermission = new RolePermission
            {
                PermissionId=dto.PermissionID,
                RoleId=dto.RoleID,
                IsActive=dto.IsActive,
                CreatedBy=id,
                ModifiedBy=id
            };

          await  _userRolePermissionrepository.AddAsync(rolePermission);

            return rolePermission;
        }

        public async Task<IEnumerable<ResponseRolePermissionDto>> GetAllRolePermissions()
        {
            var rolePermission=await _userRolePermissionrepository.GetAllAsync();

            return rolePermission.Select(rp=>new ResponseRolePermissionDto { 
            PermissionID=rp.PermissionId,
            RoleID=rp.RoleId
            });

        }
    }
}
