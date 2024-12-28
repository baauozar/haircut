using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer;
using DataLayer.Abstract;
using DataLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// =========================
// 1. Configure Services
// =========================

// Add services to the container.
builder.Services.AddControllersWithViews();

// -------------------------
// 1.1 Configure Entity Framework Core with SQL Server
// -------------------------
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------------
// 1.2 Configure Identity Services with Roles
// -------------------------
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// -------------------------
// 1.3 Configure Identity Options
// -------------------------
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => { option.LoginPath = "/Account/login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
   


// -------------------------
// 1.4 Configure Application Cookie
// -------------------------
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Path to the login page
    options.AccessDeniedPath = "/Account/AccessDenied"; // Path to the access denied page
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;

    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure cookies are only sent over HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict;
});

// -------------------------
// 1.5 Configure Authorization Policies (Optional)
// -------------------------
// Since you're using [Authorize(Roles = "Admin")], this section is optional.
// If you prefer using policies, uncomment and adjust accordingly.

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AdminOnly", policy =>
//         policy.RequireRole("Admin"));
// });

// -------------------------
// 1.6 Register Repositories (DAL)
// -------------------------
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register specific Data Access Layers
builder.Services.AddScoped<IBeautyCardInfoDal, BeautyCardInfoDal>();
builder.Services.AddScoped<IBeautyCategoryDal, BeautyCategoryDal>();
builder.Services.AddScoped<IBeautyItemsDal, BeautyItemsDal>();
builder.Services.AddScoped<IBeautyServicesItemDal, BeautyServicesItemDal>();
builder.Services.AddScoped<IBeautyServicesDal, BeautyServicesDal>();
builder.Services.AddScoped<ICompanyDal, CompanyDal>();
builder.Services.AddScoped<IContactDal, ContactDal>();
builder.Services.AddScoped<IFaqDal, FaqDal>();
builder.Services.AddScoped<IHaircutMenuCategoryDal, HaircutMenuCategoryDal>();
builder.Services.AddScoped<IHaircutMenuItemDal, HaircutMenuItemDal>();
builder.Services.AddScoped<IHaircutServicesCategoryDal, HaircutServicesCategoryDal>();
builder.Services.AddScoped<IHaircutServicesDal, HaircutServicesDal>();
builder.Services.AddScoped<IHairCutSupServicesDal, HairCutSupServicesDal>();
/*builder.Services.AddScoped<IHairCutTeammemberDal, HairCutTeammemberDal>();*/

// -------------------------
// 1.7 Register Business Services
// -------------------------
builder.Services.AddScoped<IBeautyCardInfoService, BeautyCardInfoService>();
builder.Services.AddScoped<IBeautyCategoryService, BeautyCategoryService>();
builder.Services.AddScoped<IBeautyItemsService, BeautyItemsService>();
builder.Services.AddScoped<IBeautyServicesItemService, BeautyServicesItemSrervice>();
builder.Services.AddScoped<IBeautyServices, BeautyServices>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IHaircutMenuCategoryService, HaircutMenuCategoryService>();
builder.Services.AddScoped<IHaircutMenuItemService, HaircutMenuItemService>();
builder.Services.AddScoped<IHaircutServicesCategoryService, HaircutServicesCategoryService>();
builder.Services.AddScoped<IHaircutServicesService, HaircutServicesService>();
builder.Services.AddScoped<IHairCutSupServicesService, HairCutSupServicesService>();
/*builder.Services.AddScoped<IHairCutTeammemberService, HairCutTeammemberService>();*/

// =========================
// 2. Build the Application
// =========================
var app = builder.Build();

// =========================
// 3. Configure the HTTP Request Pipeline
// =========================

if (!app.Environment.IsDevelopment())
{
    // Use Exception Handler for non-development environments
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files

// -------------------------
// 3.1 Configure Status Code Pages
// -------------------------
app.UseStatusCodePagesWithReExecute("/Account/NotFoundPage");

// -------------------------
// 3.2 Configure Routing
// -------------------------
app.UseRouting();

// -------------------------
// 3.3 Custom Middleware for 404 Handling (Optional)
// -------------------------
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        context.Request.Path = "/Account/NotFoundPage";
        await next();
    }
});

// -------------------------
// 3.4 Seed Database (Roles and Admin User)
// -------------------------


// -------------------------
// 3.5 Authentication and Authorization Middleware
// -------------------------
app.UseAuthentication(); // Must come before UseAuthorization
app.UseAuthorization();
app.Use(async (context, next) =>
{
    await next();

    // Check if response status code indicates unauthorized access
    if (context.Response.StatusCode == 403) // Access Denied
    {
        context.Response.Redirect("/Account/AccessDenied");
    }
    else if (context.Response.StatusCode == 401) // Unauthorized
    {
        context.Response.Redirect("/Account/Login");
    }
});
// -------------------------
// 3.6 Configure Endpoint Routing
// -------------------------
app.UseEndpoints(endpoints =>
{
    // Area routing
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    // Default routing
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

// =========================
// 4. Run the Application
// =========================
app.Run();
