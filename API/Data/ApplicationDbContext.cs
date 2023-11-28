using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<Account> Accounts { get; set; }
    
}
