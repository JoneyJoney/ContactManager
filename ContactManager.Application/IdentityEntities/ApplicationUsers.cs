using Microsoft.AspNetCore.Identity;
namespace ContactManager.Application.IdentityEntities
{
    public class ApplicationUsers :IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
