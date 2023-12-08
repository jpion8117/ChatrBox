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
var _context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>() ??
    throw new InvalidOperationException("Failed to retrieve database context.");

//var usersWithoutIcon = _context.Users
//    .Where(u => string.IsNullOrEmpty(u.ImageUrl))
//    .ToList();

//foreach (var user in usersWithoutIcon)
//{
//    user.QuickAssign(ImageUploader.AssignDefaultIcon());
//    _context.Users.Update(user);
//}

//var communitiesWithoutIcon = _context.Communities
//    .Where(c => string.IsNullOrEmpty(c.ImageUrl))
//    .ToList();

//foreach (var community in communitiesWithoutIcon)
//{
//    community.QuickAssign(ImageUploader.AssignDefaultIcon());
//    _context.Communities.Update(community);
//}

//_context.SaveChanges();

var cheddar = _context.Users.FirstOrDefault(u => u.UserName == "Cheddar_Chatr") ?? 
    throw new ArgumentNullException("System accound missing from database!");

Cheddar.SystemUserId = cheddar.Id;



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
