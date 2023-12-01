#nullable disable
using ChatrBox.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
        public DbSet<Message> Messages { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Use this to seed any data that requires a user, such as messages and
            //communities. If you get stuck let me know. - Josh
            var adminGuid = Guid.NewGuid().ToString();
            var chatrBoxAutomated = Guid.NewGuid().ToString();

            var superAdminRoleGuid = Guid.NewGuid().ToString();
            var adminRoleGuid = Guid.NewGuid().ToString();
            var moderatorRoleGuid = Guid.NewGuid().ToString();
            var internalSystemGuid = Guid.NewGuid().ToString();

            Chatr defaultAdmin = new Chatr();
            PasswordHasher<Chatr> passwordHasher = new PasswordHasher<Chatr>();

            defaultAdmin.Id = adminGuid;
            defaultAdmin.UserName = "admin";
            defaultAdmin.NormalizedUserName = "ADMIN";
            defaultAdmin.ImageUrl = "";
            defaultAdmin.ImageHash = "";
            defaultAdmin.Email = "example@example.com";
            defaultAdmin.EmailConfirmed = true;
            defaultAdmin.LockoutEnabled = false;
            defaultAdmin.PasswordHash = passwordHasher.HashPassword(defaultAdmin, "password");

            //generate an impossibly long password that is discarded and safeguards the
            //system account from being used.
            string sysPass = "";
            var rand = new Random();
            for (int i = 0; i < 100000;  i++) 
            {
                sysPass += (char)rand.Next(32, 126);
            }

            Chatr system = new Chatr();

            system.Id = chatrBoxAutomated;
            system.UserName = "Cheddar_Chatr";
            system.NormalizedUserName = "CHEDDAR_CHATR";
            system.ImageUrl = "";
            system.ImageHash = "";
            system.Email = "";
            system.EmailConfirmed = true;
            system.LockoutEnabled = false;
            system.PasswordHash = passwordHasher.HashPassword(system, sysPass);

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    //admin role can change site configurations and send messages
                    //to any community. Admins can also assign moderator privilages
                    //to other users but, cannot assign or revoke admin privilages.
                    new IdentityRole()
                    {
                        Id = adminRoleGuid,
                        Name = "admin",
                        NormalizedName = "ADMIN"
                    },

                    //superAdmin role can do everything an admin can plus assign 
                    //or revoke admin privilages.
                    new IdentityRole()
                    {
                        Id = superAdminRoleGuid,
                        Name = "superAdmin",
                        NormalizedName = "SUPERADMIN"
                    },

                    //Moderators can view any community (including private) to facilitate
                    //content moderation but cannot post without being a member of a
                    //community. Moderators may also delete content determined as a
                    //violation of community guidlines.
                    new IdentityRole()
                    {
                        Id = moderatorRoleGuid,
                        Name = "moderator",
                        NormalizedName = "MODERATOR"
                    },

                    //This role is reserved for internal system use. Accounts assigned this
                    //role should have login disabled to prevent abuse. No other role can
                    //assign this role to a user.
                    new IdentityRole()
                    {
                        Id = internalSystemGuid,
                        Name = "InternalSystem",
                        NormalizedName = "INTERNALSYSTEM"
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
                        RoleId = adminRoleGuid,
                        UserId = chatrBoxAutomated
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = superAdminRoleGuid,
                        UserId = chatrBoxAutomated
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = moderatorRoleGuid,
                        UserId = chatrBoxAutomated
                    },
                    new IdentityUserRole<string>()
                    {
                        RoleId = internalSystemGuid,
                        UserId = chatrBoxAutomated
                    }
                );

            modelBuilder.Entity<Chatr>()
                .HasData(defaultAdmin, system);

            modelBuilder.Entity<Community>()
                .HasData(new Community()
                {
                    Id = 1,
                    Description = "System generated community used for testing layouts",
                    Name = "Loreum Ipsum",
                    OwnerId = chatrBoxAutomated,
                    Visibility = Models.CommunityControls.Visibility.Closed,
                    ContentFilter = Models.CommunityControls.ContentFilter.NSFW,
                    ImageUrl = "",
                    ImageHash = ""
                });

            modelBuilder.Entity<Topic>()
                .HasData(new Topic
                {
                    Id = 1,
                    Description = "System generated topic used for layout testing",
                    CommunityId = 1,
                    Name = "Testing",
                    PostPermission = Models.CommunityControls.PostPermission.Closed,
                    LastPost = DateTime.Parse("1/1/2000")
                });

            //var testMessages = File.ReadAllLines("AutomatedMessages/TestMessages.txt");
            //var messages = new List<Message>();

            //for (int i = 0; i < 2; i++)
            //{
            //    messages.Add(new Message
            //    {
            //        Id = i + 1,
            //        MessagePlain = testMessages[rand.Next(testMessages.Length)],
            //        SenderId = chatrBoxAutomated,
            //        TopicId = 1
            //    });
            //}

            //modelBuilder.Entity<Message>()
            //    .HasData(messages);
        }
    }
}