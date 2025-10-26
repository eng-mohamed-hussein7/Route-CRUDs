using Application.DTOs.RoleDTOs;
using Application.ResultFolder;

namespace Application.IServices.IRoleServices;

public interface IRoleService
{
    Task<Result> CreateRoleAsync(CreateRoleDTO createRoleDTO);
    Task<Result> GetRolesAsync();
    Task<Result> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO);
    Task<Result> DeleteRoleAsync(string roleId);
    Task<Result> GetByIdRoleAsync(string roleId);
}