using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using DataLayer;
using DataLayer.Abstract;
using DataLayer.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Repositories (Dal)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));


builder.Services.AddScoped<IBeautyCardInfoDal, BeautyCardInfoDal>();
builder.Services.AddScoped<IBeautyCategoryDal, BeautyCategoryDal>();
builder.Services.AddScoped<IBeautyItemsDal, BeautyItemsDal>();
builder.Services.AddScoped<IBeautymultiItemsDal, BeautymultiItemsServiceDal>();
builder.Services.AddScoped<IBeautysServicesDal, BeautysServicesDal>();
builder.Services.AddScoped<ICompanyDal, CompanyDal>();
builder.Services.AddScoped<IContactDal, ContactDal>();
builder.Services.AddScoped<IFaqDal, FaqDal>();
builder.Services.AddScoped<IHaircutMenuCategoryDal, HaircutMenuCategoryDal>();
builder.Services.AddScoped<IHaircutMenuItemDal, HaircutMenuItemDal>();
builder.Services.AddScoped<IHaircutServicesCategoryDal, HaircutServicesCategoryDal>();
builder.Services.AddScoped<IHaircutServicesDal, HaircutServicesDal>();
builder.Services.AddScoped<IHairCutSupServicesDal, HairCutSupServicesDal>();
builder.Services.AddScoped<IHairCutTeammemberDal, HairCutTeammemberDal>();
//////////////////////////////
///// Register Business Services
//////////////////////////////
builder.Services.AddScoped<IBeautyCardInfoService, BeautyCardInfoService>();
builder.Services.AddScoped<IBeautyCategoryService, BeautyCategoryService>();
builder.Services.AddScoped<IBeautyItemsService, BeautyItemsService>();
builder.Services.AddScoped<IBeautymultiItemsService, BeautymultiItems>();
builder.Services.AddScoped<IBeautysServices, BeautysServices>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IFaqService, FaqService>();
builder.Services.AddScoped<IHaircutMenuCategoryService, HaircutMenuCategoryService>();
builder.Services.AddScoped<IHaircutMenuItemService, HaircutMenuItemService>();
builder.Services.AddScoped<IHaircutServicesCategoryService, HaircutServicesCategoryService>();
builder.Services.AddScoped<IHaircutServicesService, HaircutServicesService>();
builder.Services.AddScoped<IHairCutSupServicesService, HairCutSupServicesService>();
builder.Services.AddScoped<IHairCutTeammemberService, HairCutTeammemberService>();


///





builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen();*/
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
