using Application.DTOs.RoleDTOs;
using Application.IServices.IRoleServices;
using Application.ResultFolder;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.RoleServices;


public class RoleService : IRoleService
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleService(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result> CreateRoleAsync(CreateRoleDTO createRoleDTO)
    {
        try
        {
            if (await _roleManager.RoleExistsAsync(createRoleDTO.RoleName))
                return Result.Failure("Role already exist.");

            var role = new ApplicationRole
            {
                Name = createRoleDTO.RoleName
            };

            var result = await _roleManager.CreateAsync(role);

            return Result.Success("Role added successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to create role: {ex.Message}", status: StatusResult.Failed);
        }

    }

    public async Task<Result> DeleteRoleAsync(string roleId)
    {
        try
        {

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return Result.Failure("Role already deleted.");

            role.IsDeleted = true;
            var result = await _roleManager.UpdateAsync(role);

            return Result.Success("Role Delete successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to create role: {ex.Message}", status: StatusResult.Failed);
        }
    }

    public async Task<Result> GetByIdRoleAsync(string roleId)
    {
        try
        {

            var role = await _roleManager.Roles
                              .Where(r => r.Id == roleId && !r.IsDeleted)
                              .Select(r => new RoleDetailsDTO
                              {
                                  RoleId = r.Id,
                                  RoleName = r.Name
                              })
                              .FirstOrDefaultAsync();
            if (role == null)
            {

                return Result.NotExists("Role Does not Exists.");
            }

            return Result.Success("Role retrieve successfully.", role);
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to get role: {ex.Message}", status: StatusResult.Failed);
        }
    }

    public async Task<Result> GetRolesAsync()
    {
        try
        {
            var roles = _roleManager.Roles
          .Where(r => !r.IsDeleted)
          .Select(role => new RoleDetailsDTO
          {
              RoleId = role.Id,
              RoleName = role.Name
          }).ToList();
            if (roles.Count == 0)
            {
                return Result.NotExists("Roles not Exists.");

            }
            return Result.Success("Roles retrieve successfully.", roles);
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to get roles: {ex.Message}", status: StatusResult.Failed);
        }

    }

    public async Task<Result> UpdateRoleAsync(UpdateRoleDTO updateRoleDTO)
    {
        try
        {

            var role = await _roleManager.FindByIdAsync(updateRoleDTO.RoleId.ToString());

            if (role == null)
                return Result.Failure("Role does not exist.");

            var existingRole = await _roleManager.FindByNameAsync(updateRoleDTO.NewRoleName);

            if (existingRole != null && existingRole.Id != role.Id)
                return Result.Failure("Role does not exist.");

            role.Name = updateRoleDTO.NewRoleName;

            var result = await _roleManager.UpdateAsync(role);

            return Result.Success("Role Update successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Failed to create role: {ex.Message}", status: StatusResult.Failed);
        }
    }
}