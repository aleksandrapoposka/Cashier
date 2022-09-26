﻿using Entities.User;
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
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN".ToUpper(),
                    ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210"
                });

            //create super admin user
            var superAdminFilip = new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb1", // primary key
                UserName = "filip@admin.com",
                NormalizedUserName = "filip@admin.com".ToUpper(),
                DateOfBirth = new DateTime(1995, 4, 25),
                Email = "filip@admin.com",
                FirstName = "Filip",
                LastName = "Cvetkovski",
                NormalizedEmail = "filip@admin.com".ToUpper(),
                PhoneNumber = "077511123"
            };
            var hasher = new PasswordHasher<ApplicationUser>();
            superAdminFilip.PasswordHash = hasher.HashPassword(superAdminFilip, "Password");

            modelBuilder.Entity<ApplicationUser>().HasData(superAdminFilip);

            var superAdminAleks = new ApplicationUser
            {
                Id = "8e445123-a24d-4543-a6c6-9443d048cdc1", // primary key
                UserName = "aleksandra@admin.com",
                NormalizedUserName = "aleksandra@admin.com".ToUpper(),
                DateOfBirth = new DateTime(1995, 4, 25),
                Email = "aleksandra@admin.com",
                FirstName = "Aleksandra",
                LastName = "Poposka",
                NormalizedEmail = "aleksandra@admin.com".ToUpper(),
                PhoneNumber = "077511166"
            };
            superAdminAleks.PasswordHash = hasher.HashPassword(superAdminAleks, "Password");

            modelBuilder.Entity<ApplicationUser>().HasData(superAdminAleks);

            //seed roles and users
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb1"
                });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445123-a24d-4543-a6c6-9443d048cdc1"
                });
        }
    }
}