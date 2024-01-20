using FDBlog.Dal.UnitOfWorks;
using FDBlog.Entity.Dtos.Users;
using FDBlog.Entity.Entities;
using FDBlog.Service.Services.Abstractions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDBlog.Service.Services.Concrete
{
    public class DashboardService:IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<List<int>> GetYearlyArticleCounts()
        {
            var articles = await _unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted);

            var startDate = DateTime.Now.Date;//bulunduğumuz günü bulduk burdan yılı çekicez.
            startDate = new DateTime(startDate.Year, 1, 1);//bulunduğumuz yılı aldık ve 1. ayın 1.gününü seçtik.

            List<int> datas = new();

            for(int i=1; i<=12;i++)
            {
                var startedDate = new DateTime(startDate.Year, i, 1);
                var endedDate = startDate.AddMonths(1);
                var data = articles.Where(x => x.CreatedDate >= startedDate && x.CreatedDate < endedDate).Count();
                datas.Add(data);//döngüden gelen aya göre o aydaki makale sayısını ekleyen kod bloğu
            }
            return datas;
        }
        public async Task<int> GetTotalArticleCount()
        {
            var articleCount = await _unitOfWork.GetRepository<Article>().CountAsync();
            return articleCount;
        }
        public async Task<int> GetTotalCategoryeCount()
        {
            var categoryCount = await _unitOfWork.GetRepository<Category>().CountAsync();
            return categoryCount;
        }
        public async Task<int> GetTotalUserCount()
        {
            var userRolesCount = await _unitOfWork.GetRepository<AppUserRole>().CountAsync(x => x.RoleId == 3);
            return userRolesCount;
        }
        public async Task<int> GetTotalAdminCount()
        {
            var adminRolesCount = await _unitOfWork.GetRepository<AppUserRole>().CountAsync(x => x.RoleId == 2);
            return adminRolesCount;
        }
    }
}
