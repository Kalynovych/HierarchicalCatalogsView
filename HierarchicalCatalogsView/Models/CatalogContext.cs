using Microsoft.EntityFrameworkCore;

namespace HierarchicalCatalogsView.Models
{
    public class CatalogContext : DbContext
    {
        public DbSet<Catalog> Catalogs { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>().HasOne(p => p.ParentCatalog).WithMany(c => c.ChildCatalogs)
                .HasForeignKey(p => p.ParentId).IsRequired(false);

            modelBuilder.Entity<Catalog>().HasData(InitializeDb());
        }

        private Catalog[] InitializeDb()
        {
            return new Catalog[]
            {
                new Catalog() { CatalogId = 1, CatalogName = "Creating Digital Images", ParentId = null },
                new Catalog() { CatalogId = 2, CatalogName = "Resources", ParentId = 1 },
                new Catalog() { CatalogId = 3, CatalogName = "Evidence", ParentId = 1 },
                new Catalog() { CatalogId = 4, CatalogName = "Graphic Products", ParentId = 1 },
                new Catalog() { CatalogId = 5, CatalogName = "Primary Sources", ParentId = 2 },
                new Catalog() { CatalogId = 6, CatalogName = "Secondary Sources", ParentId = 2 },
                new Catalog() { CatalogId = 7, CatalogName = "Process", ParentId = 4 },
                new Catalog() { CatalogId = 8, CatalogName = "Final Product", ParentId = 4 }
            };
        }
    }
}
