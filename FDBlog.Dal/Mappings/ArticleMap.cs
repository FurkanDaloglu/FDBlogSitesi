using FDBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FDBlog.Dal.Mappings
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasData(new Article
            {
                Id = 1,
                Title = "Asp.net Core Deneme Makale",
                Content = "Yazılacak",
                ViewCount = 15,
                CategoryId = 1,
                ImageId = 1,
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId= 1
            },
            new Article
            {
                Id = 2,
                Title = "Visual Studio Deneme Makale",
                Content = "Yazılacak",
                ViewCount = 15,
                CategoryId = 2,
                ImageId = 2,
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                UserId=2
            });

        }
    }
}
