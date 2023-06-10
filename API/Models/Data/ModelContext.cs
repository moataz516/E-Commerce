using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace E_Website.Models.Data
{
    public class ModelContext : IdentityDbContext<applicationUser>
    {

        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        public DbSet<userCard> userCards { get; set; }
        public DbSet<product> Products { get; set; }
        public DbSet<category> Categories { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<orderDetails> orderDetails { get; set; }
        public DbSet<productImage> productImages { get; set; }
        //public DbSet<productCategory> ProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<productCategory>().HasKey(x => new { x.categoryId, x.productId });
            builder.Entity<product>().HasOne(a=>a.category).WithMany(b=>b.product).HasForeignKey(b=>b.categoryId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<applicationUser>().HasMany(a=>a.orders).WithOne(b=>b.user).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<applicationUser>().HasOne(a=>a.userCard).WithOne(b=>b.user).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<userCard>().Property(p => p.wallet).HasDefaultValue(1000);
            builder.Entity<product>().HasMany(p=>p.productImages).WithOne(i=>i.product).OnDelete(DeleteBehavior.Cascade);
        }

    }


}
