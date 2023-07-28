using DempAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DempAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
