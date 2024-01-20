using FDBlog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDBlog.Entity.Entities
{
    public class ArticleVisitor:IEntityBase
    {
        public ArticleVisitor()
        {
            
        }
        public ArticleVisitor(int articleId,int visitorId)
        {
            ArticleId = articleId;
            VisitorId = visitorId;
        }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
    }
}
