using Application.Services.RoleService.DTOs;

namespace Application.Services.RoleService
{
    public interface IRoleService
    {
        public Task<List<GetAllRolesDto>> GetAllRoles();
    }
}
