#nullable disable
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatrBox.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public new DbSet<Chatr> Users { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<CommunityUser> CommunityUsers { get; set; }
        public DbSet<ChatrIcon> ChatrIcons { get; set; }
        public DbSet<CommunityIcon> CommunityIcons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topic { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChatrIcon>()
                .HasOne<Chatr>(i => i.Chatr)
                .WithOne(c => c.Icon)
                .HasForeignKey<Chatr>(i => i.IconId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommunityIcon>()
                .HasOne<Community>(i => i.Community)
                .WithOne(c => c.Icon)
                .HasForeignKey<Community>(i => i.IconId)
                .OnDelete(DeleteBehavior.Cascade);

            //Use this to seed any data that requires a user, such as messages and
            //communities. If you get stuck let me know. - Josh
            var adminGuid = Guid.NewGuid().ToString();

            var superAdminRoleGuid = Guid.NewGuid().ToString();
            var adminRoleGuid = Guid.NewGuid().ToString();
            var moderatorRoleGuid = Guid.NewGuid().ToString();
            var userRoleGuid = Guid.NewGuid().ToString();

            var defaultAdmin = new Chatr()
            {
                Id = adminGuid,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@example.com",
                LockoutEnabled = false,
                EmailConfirmed = true
            };

            PasswordHasher<Chatr> passwordHasher = new PasswordHasher<Chatr>();
            defaultAdmin.PasswordHash = passwordHasher.HashPassword(defaultAdmin, "password");

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole()
                    {
                        Id = adminRoleGuid,
                        Name = "admin",
                        NormalizedName = "admin"
                    },
                    new IdentityRole()
                    {
                        Id = superAdminRoleGuid,
                        Name = "superAdmin",
                        NormalizedName = "superAdmin"
                    },
                    new IdentityRole()
                    {
                        Id = moderatorRoleGuid,
                        Name = "moderator",
                        NormalizedName = "moderator"
                    },
                    new IdentityRole()
                    {
                        Id = userRoleGuid,
                        Name = "user",
                        NormalizedName = "user"
                    }
                );

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                    new IdentityUserRole<string>()
                    {
                        RoleId = adminRoleGuid,
                        UserId = adminGuid
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = superAdminRoleGuid,
                        UserId = adminGuid
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = moderatorRoleGuid,
                        UserId = adminGuid
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = userRoleGuid,
                        UserId = adminGuid
                    }
                );

            modelBuilder.Entity<Chatr>()
                .HasData(defaultAdmin);
        }
    }
}