using BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.GetByIdArticles
{
    public class GetByIdArticleQueryResponse
    {
        public string Title { get; set; }
        public string Content { get; set; }
        
    }
}
