using FDBlog.Dal.Context;
using FDBlog.Dal.Repositories.Abstractions;
using FDBlog.Dal.Repositories.Concretes;
using FDBlog.Dal.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FDBlog.Dal.Extensions
{
    public static class DalLayerExtension
    {
        public static void LoadDalLayerExtension(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
