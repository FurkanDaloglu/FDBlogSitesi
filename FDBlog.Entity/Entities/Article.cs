using FDBlog.Core.Entities;

namespace FDBlog.Entity.Entities
{
    public class Article:EntityBase
    {
        public Article()
        {
            
        }
        public Article(string title, string content, int userId, string createdBy, int categoryId, int imageId)
        {
            Title = title;
            Content = content;
            UserId = userId;
            CategoryId = categoryId;
            ImageId = imageId;
            CreatedBy = createdBy;
        }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;

        //navigation prop
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public ICollection<ArticleVisitor> articleVisitors { get; set; }
    }
}
