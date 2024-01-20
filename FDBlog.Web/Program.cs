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
    //bunlarý test aþamasýnda þifreyi 123456 yapabilmek için kolaylýk olsun diye yaptýk ilerleyen aþamada kaldýrýlacak.
})
    .AddRoleManager<RoleManager<AppRole>>()
    .AddErrorDescriber<CustomIdentityErrorDescriber>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Admin/Auth/Login");
    //burasý herhangi birinin admin panel baðlantý ismini bilse bile oraya yönleneceði zaman login olmadýysa eðer verilen yönlendirmedeki sayfaya direk yönledirir.
    config.LogoutPath = new PathString("/Admin/Auth/Loguot");
    //kullanýcý logaut olmak istediðinde buraya yönlendirir.
    config.Cookie = new CookieBuilder
    {
        Name = "FDBlog",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest //test aþamasýnda kolaylýk için hem http hemde https kullanýyoruz ama proje canlýya çýkacaðý zaman Always kullanýlacak. 
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan=TimeSpan.FromDays(7);//Cookie nin tutulma süresini belirler.
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
    //örnek olarak admin olan birisi superadmin olan birisinin kullanabileceði bir özelliði kullanmayý denerse ona uyarý verecek sayfa bu olacak.
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

app.UseAuthentication();//useauthorizationun üstünde olmasý lazým çünkü kullanýcý sisteme login olduðunu bilmeden authorization uygulayamayýz.
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
