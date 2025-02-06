using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopApp.DAL.Models;

namespace WebShopApp.DAL
{
    public class Context : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> Items { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>(or =>
            {
                or.HasKey(or => new { or.Id });
                or.Property(or => or.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<OrderItems>(oi =>
            {
                oi.HasKey(oi => new { oi.Id });
                oi.Property(oi => oi.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ShippingAddress>(sh =>
            {
                sh.HasKey(sh => new { sh.Id });
                sh.Property(sh => sh.Id).ValueGeneratedOnAdd();
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
