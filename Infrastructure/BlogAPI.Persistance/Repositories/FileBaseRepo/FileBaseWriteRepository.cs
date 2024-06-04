using BlogAPI.Application.Repositories.FileBaseRepo;
using BlogAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories.FileBaseRepo
{
    public class FileBaseWriteRepository : WriteRepository<BlogAPI.Domain.Entities.BaseFile>, IFileBaseWriteRepository
    {
        public FileBaseWriteRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
