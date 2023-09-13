using Microsoft.EntityFrameworkCore;

namespace HNGBACKENDTrack.Model
{
    public class HNGxDBContext : DbContext
    {
        public HNGxDBContext(DbContextOptions<HNGxDBContext> options) : base(options)
        {
            
        }

        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
    }
}
