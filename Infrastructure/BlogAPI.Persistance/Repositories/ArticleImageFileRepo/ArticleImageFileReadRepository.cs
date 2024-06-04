using BlogAPI.Application.Repositories;
using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories.ArticleImageFileRepo
{
    public class ArticleImageFileReadRepository : ReadRepository<Domain.Entities.ArticleImageFile>, IArticleImageFileReadRepository
    {
        public ArticleImageFileReadRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
