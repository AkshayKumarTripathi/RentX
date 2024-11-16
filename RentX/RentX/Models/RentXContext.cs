using Microsoft.EntityFrameworkCore;

namespace RentX.Models
{
    public class RentXContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }

        public RentXContext(DbContextOptions<RentXContext> options) : base(options) { }

        
    }
}
