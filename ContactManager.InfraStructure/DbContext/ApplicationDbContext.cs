using ContactManager.Application.IdentityEntities;
using ContactManager.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.InfraStructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUsers,ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
                

        }

        public DbSet<Country> Countries { get; set; }
    }

}
