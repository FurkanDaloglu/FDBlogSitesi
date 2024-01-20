using FDBlog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FDBlog.Dal.Mappings
{
    public class ImageMap:IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasData(new Image
            {
                Id = 1,
                FileName = "images/testimage",
                FileType = "jpg",
                CreatedBy = "Admin Test",
                CreatedDate = DateTime.Now,
                IsDeleted = false
            },
             new Image
             {
                 Id = 2,
                 FileName = "images/vstest",
                 FileType = "png",
                 CreatedBy = "Admin Test",
                 CreatedDate = DateTime.Now,
                 IsDeleted = false
             });
        }

        
    }
}
