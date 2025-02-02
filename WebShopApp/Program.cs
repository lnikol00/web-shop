using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopApp.DAL;
using WebShopApp.DAL.Models;
using WebShopApp.Infrastructure.Authorization;
using WebShopApp.Infrastructure.Interface;
using WebShopApp.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddRazorPages()
                .AddRazorPagesOptions(options => { options.Conventions.AllowAnonymousToPage("/Account/Login"); })
                .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<Context>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddHttpClient();

builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(480);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

builder.Services.AddScoped<IRepository, EFRepository<Context>>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/AccessDenied";
});

builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("api", p =>
    {
        p.RequireAuthenticatedUser();
        p.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
    });

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(config =>
{
    config.SignIn.RequireConfirmedEmail = false;
    config.Password = new PasswordOptions { RequireDigit = false, RequiredUniqueChars = 0, RequireNonAlphanumeric = false, RequireLowercase = false, RequireUppercase = false, RequiredLength = 8 };
})
      .AddApiEndpoints()
      .AddEntityFrameworkStores<Context>();

var app = builder.Build();

app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(name: "MyAreaSelfService", areaName: "SelfService", pattern: "SelfService/{controller}/{action}/{id?}", new { controller = "Home", action = "Index" });


    endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}", new { controller = "Home", action = "Index" });

    endpoints.MapControllers();
    endpoints.MapGroup("api/auth").MapIdentityApi<ApplicationUser>();
});

app.Run();
