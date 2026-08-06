using Microsoft.EntityFrameworkCore;
using Principal.Models;

namespace Principal.Repositories
{
    public class PrincipalDbContext : DbContext
    {
        public DbSet<Programador> Programadores { get; set; }
        public PrincipalDbContext(DbContextOptions<PrincipalDbContext> options) : base(options)
        {
        }

        
    }
}
