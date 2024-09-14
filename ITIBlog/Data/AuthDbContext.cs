using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITIBlog.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Seed Roles (User , Admin , Super Admin)
            var superAdminRoleId = "e7d315fd-55c2-46b3-bbab-2dead4a95e5d";
            var adminRoleId = "32bea4af-9dbf-435e-8240-202dcf1913ee";
            var userRoleId = "a18eb4be-2f95-40d2-a2f9-f989f73c0e4b";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            //Seed Super Admin
            var superAdminId = "5cc46e43-7d71-49aa-b139-778f795a944c";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@itiblog.com",
                Email = "superadmin@itiblog.com",
                NormalizedEmail = "superadmin@itiblog.com".ToUpper(),
                NormalizedUserName = "superadmin@itiblog.com".ToUpper(),
                Id = superAdminId,

            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");
            builder.Entity<IdentityUser>().HasData(superAdminUser);
            //Add All Roles To Super Admin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
