using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Domain.Entities;
using BlogAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories.ArticleRepo
{
    public class ArticleWriteRepository : WriteRepository<Article>, IArticleWriteRepository
    {
        public ArticleWriteRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
