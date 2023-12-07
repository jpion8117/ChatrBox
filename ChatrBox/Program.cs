using ChatrBox.Data;
using ChatrBox.Models;
using Markdig.Extensions.AutoLinks;
using Markdig.Extensions.MediaLinks;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChatrBox.Areas.API.Controllers;
using ChatrBox.Models.CommunityControls;
using ChatrBox.CoreComponents.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
        .UseSqlServer(connectionString)
        .UseLazyLoadingProxies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Chatr, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddControllersWithViews();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 2;
});

var app = builder.Build();


ImageUploader.HostPath = app.Environment.WebRootPath;
AdminController.HomePath = app.Environment.ContentRootPath;

//database housekeeping tasks
var context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>() ?? 
    throw new InvalidOperationException("Failed to retrieve database context.");

var usersWithoutIcon = context.Users
    .Where(u => string.IsNullOrEmpty(u.ImageUrl))
    .ToList();

foreach (var user in usersWithoutIcon)
{
    user.QuickAssign(ImageUploader.AssignDefaultIcon());
    context.Users.Update(user);
}

var communitiesWithoutIcon = context.Communities
    .Where(c => string.IsNullOrEmpty(c.ImageUrl))
    .ToList();

foreach (var community in communitiesWithoutIcon)
{
    community.QuickAssign(ImageUploader.AssignDefaultIcon());
    context.Communities.Update(community);
}

context.SaveChanges();

var communities = context.Communities
    .ToList();
var communitiesWithoutSystemTopics = new List<Community>();

foreach (var community in communities)
    if(community.MissingSystemTopics().Any())
        communitiesWithoutSystemTopics.Add(community);

var cheddar = context.Users.FirstOrDefault(u => u.UserName == "Cheddar_Chatr") ?? 
    throw new ArgumentNullException("System accound missing from database!");

Cheddar.SystemUserId = cheddar.Id;

foreach (var community in communitiesWithoutSystemTopics)
{
    var missingSystemTopics = community.MissingSystemTopics();

    foreach (var missingSystemTopic in missingSystemTopics)
    {
        switch (missingSystemTopic)
        {
            case "public":
                var publicTopic = new Topic
                {
                    CommunityId = community.Id,
                    DisplayOrder = -4,
                    Description = "Anything only community managers and their appointed " +
                        "moderators may post to this topic. Any Chat'r may view this topic " +
                        "unless the community is set to closed. This topic is auto generated " +
                        "and cannot be removed or modified.",
                    PostPermission = PostPermission.Closed,
                    LastPost = DateTime.Now
                };

                publicTopic.SetSystemTopic("public");
                context.Topics.Add(publicTopic);
                context.SaveChanges();

                var welcomeMessage = new Message
                {
                    SenderId = cheddar.Id,
                    Timestamp = DateTime.Now,
                    TopicId = publicTopic.Id,
                    IsSystem = true,
                    MessagePlain = $"# Welcome to {community.Name} \n ### Please read any community " +
                        $"rules below before joining"
                };

                var mgrMessage = new Message
                {
                    SenderId = cheddar.Id,
                    Timestamp = DateTime.Now,
                    TopicId = publicTopic.Id,
                    IsSystem = true,
                    IsHidden = true,
                    MessagePlain = $"As the community manager or moderator you are the only one who can see this " +
                        $"message. This topic is visible to any registerd Chat'r unless your community is set to " +
                        $"closed. Think of this as the front page of your Chat'r community! Tell visitors why they " +
                        $"may want to join your community, lay down some ground rules, what ever you choose! The " +
                        $"messages above and below this are like the header and footer of the page. Anything you " +
                        $"post in here will show up between these two messages"
                };

                var joinMessage = new Message
                {
                    SenderId = cheddar.Id,
                    Timestamp = DateTime.Now,
                    TopicId = publicTopic.Id,
                    IsSystem = true,
                    IsSticky = true,
                    MessagePlain = $"<button class=\"btn btn-primary btn-join-community w-100\" id=\"JoinCommunity_{community.Id}\">Join {community.Name}</button>"
                };

                context.Messages.Add(welcomeMessage);
                context.Messages.Add(mgrMessage);
                context.Messages.Add(joinMessage); 
                context.SaveChanges();

                break;

            case "community-notifications":
                
                var notificationsTopic = new Topic
                {
                    CommunityId = community.Id,
                    DisplayOrder = -3,
                    Description = "Anything only community managers and their appointed " +
                        "moderators may post to this topic. Any Chat'r may view this topic " +
                        "unless the community is set to closed. This topic is auto generated " +
                        "and cannot be removed or modified.",
                    PostPermission = PostPermission.Closed,
                    LastPost = DateTime.Now
                };

                notificationsTopic.SetSystemTopic("community-notifications");
                context.Topics.Add(notificationsTopic);
                context.SaveChanges();

                var notificationExplainer = new Message
                {
                    SenderId = cheddar.Id,
                    Timestamp = DateTime.Now,
                    TopicId = notificationsTopic.Id,
                    IsSystem = true,
                    MessagePlain = "## Welcome to your notification center! \n " +
                        "* Here you will find requests from users to join your community! You if you " +
                        "click accept, they will be allowed as full members of the community and recieve " +
                        "all the benefits that come with that. \n * Think of this topic as the central " +
                        "notification center of your community. \n * only you and those you appoint as " +
                        "moderators will see this topic and any messages in here. \n * You cannot post " +
                        "anything here!"
                };

                context.Messages.Add(notificationExplainer);
                context.SaveChanges();

                break;
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Configures the markdown pipline used to convert markdown written in messages
//to HTML to be rendered to the user's screen
Message.MarkdownPipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseBootstrap()
    .DisableHtml()
    .Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "config",
    pattern: "{area:exists}/{controller=Communities}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
