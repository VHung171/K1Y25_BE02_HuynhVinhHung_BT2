using K1Y25_BE02_HuynhVinhHung_BT2.Models;
using Microsoft.EntityFrameworkCore;

namespace K1Y25_BE02_HuynhVinhHung_BT2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AllowAccess> AllowAccesses { get; set; }
        public DbSet<Intern> Interns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Định nghĩa quan hệ 1-1 giữa User và Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Định nghĩa quan hệ 1-N giữa Role và AllowAccess
            modelBuilder.Entity<AllowAccess>()
                .HasOne(a => a.Role)
                .WithMany(r => r.AllowAccesses)
                .HasForeignKey(a => a.RoleId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
