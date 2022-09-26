using Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cashier.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //create super admin role
            var superAdminRole = new IdentityRole
            {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin".ToUpper()
            };
            modelBuilder.Entity<IdentityRole>().HasData(superAdminRole);
        }
    }
}