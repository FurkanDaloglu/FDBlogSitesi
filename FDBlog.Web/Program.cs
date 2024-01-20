using FDBlog.Dal.Context;
using FDBlog.Dal.Extensions;
using FDBlog.Entity.Entities;
using FDBlog.Service.Describers;
using FDBlog.Service.Extensions;
using FDBlog.Web.Filters.ArticleVisitors;
using Microsoft.AspNetCore.Identity;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);
builder.Services.LoadDalLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews(opt=>
{
    opt.Filters.Add<ArticleVisitorFilter>();
})
    .AddNToastNotifyToastr(new ToastrOptions()
    {
        PositionClass=ToastPositions.TopRight,
        TimeOut=3000
    })
    .AddRazorRuntimeCompilation();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    //bunlar� test a�amas�nda �ifreyi 123456 yapabilmek i�in kolayl�k olsun diye yapt�k ilerleyen a�amada kald�r�lacak.
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    //buras� herhangi birinin admin panel ba�lant� ismini bilse bile oraya y�nlenece�i zaman login olmad�ysa e�er verilen y�nlendirmedeki sayfaya direk y�nledirir.
    config.LogoutPath = new PathString("/Admin/Auth/Loguot");
    //kullan�c� logaut olmak istedi�inde buraya y�nlendirir.
    config.Cookie = new CookieBuilder
    {
        Name = "FDBlog",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //test a�amas�nda kolayl�k i�in hem http hemde https kullan�yoruz ama proje canl�ya ��kaca�� zaman Always kullan�lacak. 
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan=TimeSpan.FromDays(7);//Cookie nin tutulma s�resini belirler.
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
    //�rnek olarak admin olan birisi superadmin olan birisinin kullanabilece�i bir �zelli�i kullanmay� denerse ona uyar� verecek sayfa bu olacak.
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();//
app.UseRouting();

app.UseAuthentication();//useauthorizationun �st�nde olmas� laz�m ��nk� kullan�c� sisteme login oldu�unu bilmeden authorization uygulayamay�z.
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
        );
    endpoints.MapDefaultControllerRoute();
});

app.Run();
