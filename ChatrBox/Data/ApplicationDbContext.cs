#nullable disable
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
        public DbSet<ChatrIcon> Icons { get; set; }
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
        }
    }
}