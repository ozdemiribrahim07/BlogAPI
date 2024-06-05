using BlogAPI.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.GetAllArticles
{
    public class GetAllArticlesQueryResponse
    {
        public int TotalCount { get; set; }
        public object Articles { get; set; }

    }
}
