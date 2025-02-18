using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventManagement.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using EventManagement.Models;
using EventManagement;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("EventManagementContextConnection")
    ?? throw new InvalidOperationException("Connection string 'EventManagementContextConnection' not found.");

builder.Services.AddDbContext<EventManagementContext>(options =>
    options.UseNpgsql(connectionString) 
           .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information));

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EventManagementContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddMvc().AddViewOptions(options => options.HtmlHelperOptions.ClientValidationEnabled = true);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/NewHome/AccessDenied";
});

builder.Services.AddHostedService<EventStatusUpdater>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NewHome}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
