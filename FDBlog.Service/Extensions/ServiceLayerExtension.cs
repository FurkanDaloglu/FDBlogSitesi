using FDBlog.Dal.Context;
using FDBlog.Dal.Repositories.Abstractions;
using FDBlog.Dal.Repositories.Concretes;
using FDBlog.Dal.UnitOfWorks;
using FDBlog.Service.FluentValidations;
using FDBlog.Service.Helpers.Images;
using FDBlog.Service.Services.Abstractions;
using FDBlog.Service.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FDBlog.Service.Extensions
{
    public static class ServiceLayerExtension
    {
        public static void LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IArticleService,ArticleService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IImageHelper,ImageHelper>();
            services.AddScoped<IDashboardService,DashboardService>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            //kullanıcıyı bulmayı sağlayan kısım
            services.AddAutoMapper(assembly);

            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();//service katmanı içindeki tüm abstractvalidatorleri bulup otomatik tanımlıyor
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            });
        }
    }
}
