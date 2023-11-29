using ChatrBox.Data;
using ChatrBox.Models;
using Markdig.Extensions.AutoLinks;
using Markdig.Extensions.MediaLinks;
using Markdig;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

ImageUploader.HostPath = app.Environment.WebRootPath;

//Configures the markdown pipline used to convert markdown written in messages
//to HTML to be rendered to the user's screen
Message.MarkdownPipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseBootstrap()
    .UseMediaLinks(new MediaOptions
    {
        AddControlsProperty = true
    })
    .UseAutoLinks(new AutoLinkOptions
    {
        UseHttpsForWWWLinks = true
    })
    .EnableTrackTrivia()
    .UseAutoIdentifiers()
    .UseEmojiAndSmiley(true)
    .UseGenericAttributes()
    .UseCustomContainers()
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
