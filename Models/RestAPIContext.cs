using Microsoft.EntityFrameworkCore;

namespace RestAPI.Models
{
    public class RestAPIContext : DbContext
    {
        public RestAPIContext(DbContextOptions<RestAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Building> Building { get; set; }
    }
}