using BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Repositories.ArticleRepo
{
    public interface IArticleReadRepository : IReadRepository<Article>
    {


    }
}
