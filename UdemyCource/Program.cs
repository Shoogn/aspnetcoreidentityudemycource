using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UdemyCource.Models;
using UdemyCource.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BaseDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BaseDBConnection"));
});
builder.Services.AddAuthentication().AddCookie();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.User.RequireUniqueEmail = true;

    options.Lockout.AllowedForNewUsers = false;
    options.Lockout.DefaultLockoutTimeSpan = System.TimeSpan.FromMinutes(3); // The Default Lock time is 5 minutes
    options.Lockout.MaxFailedAccessAttempts = 5;

})
    .AddEntityFrameworkStores<BaseDBContext>()
.AddDefaultTokenProviders();


builder.Services.AddSingleton<IContentPageDetailsServices, MemoryContentPageDetailsRepository>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Accounts/SignInUser";
    options.AccessDeniedPath = "/Accounts/AccessDenied";
});

#region Route Option
// Be sure to comment this out when you work with Email Confirmation
// because this will change the shape of the Token and
// that is because the Token is basr64 string

//builder.Services.Configure<RouteOptions>(options =>
//{
//    options.LowercaseQueryStrings = true;
//    options.LowercaseUrls = true;
//});
#endregion

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
