using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories.ArticleImageFileRepo
{
    public class ArticleImageFileWriteRepo : WriteRepository<Domain.Entities.ArticleImageFile>, IArticleImageFileWriteRepository
    {
        public ArticleImageFileWriteRepo(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
