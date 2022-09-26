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
            
            //Seeding a 'SuperAdmin' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole 
                { 
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                    Name = "SuperAdmin", 
                    NormalizedName = "SUPERADMIN".ToUpper(), 
                    ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210" 
                });

            //password hasher helper
            var hasher = new PasswordHasher<ApplicationUser>();
            var superAdmin = new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                UserName = "aleksandra@admin.com",
                NormalizedUserName = "aleksandra@admin.com".ToUpper(),
                DateOfBirth = new DateTime(1995, 4, 25),
                Email = "aleksandra@admin.com",
                FirstName = "Aleksandra",
                LastName = "Poposka",
                NormalizedEmail = "aleksandra@admin.com".ToUpper(),
                PhoneNumber = "077503237"
            };

            superAdmin.PasswordHash = hasher.HashPassword(null, "Password");

            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<ApplicationUser>().HasData(superAdmin);

            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );


        }
    }
}