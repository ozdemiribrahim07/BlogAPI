using BlogAPI.Application.Repositories.CategoryRepo;
using BlogAPI.Domain.Entities;
using BlogAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories.CategoryRepo
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(BlogContext blogContext) : base(blogContext)
        {
        }
    }
}
