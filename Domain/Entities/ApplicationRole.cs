using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationRole: IdentityRole
{
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
