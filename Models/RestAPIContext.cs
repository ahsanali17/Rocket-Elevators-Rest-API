using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
    public class RestAPIContext : DbContext
    {
        public RestAPIContext(DbContextOptions<RestAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<Elevator> Elevators { get; set; }
        public DbSet<Lead> Leads { get; set; }
    }
}