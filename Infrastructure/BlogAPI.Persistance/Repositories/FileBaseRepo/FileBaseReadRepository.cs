using BlogAPI.Application.Repositories.FileBaseRepo;
using BlogAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories.FileBaseRepo
{
    public class FileBaseReadRepository : ReadRepository<BlogAPI.Domain.Entities.BaseFile>, IFileBaseReadRepository
    {
        public FileBaseReadRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
