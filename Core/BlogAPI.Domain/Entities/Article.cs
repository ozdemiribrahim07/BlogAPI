using BlogAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Domain.Entities
{
    public class Article : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ArticleImageFile> ArticleImageFiles { get; set; }

    }
}
