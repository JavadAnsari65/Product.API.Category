using Microsoft.EntityFrameworkCore;
using Product.API.Category.Infrastructure.Entities;

namespace Product.API.Category.Infrastructure.Configuration
{
    public class CategoryDbContext:DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> options):base(options)
        {
            
        }

        public DbSet<CategoryEntity> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Product.Category;Integrated Security=True; MultipleActiveResultSets=True; Trust Server Certificate=True");
            }
        }
    }

}
