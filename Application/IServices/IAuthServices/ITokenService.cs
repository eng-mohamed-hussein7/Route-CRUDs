using Domain.Entities;

namespace Application.IServices.IAuthServices;

public interface ITokenService
{
    Task<string> GenerateTokenAsync(ApplicationUser user);
}
