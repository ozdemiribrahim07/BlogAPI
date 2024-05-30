using BlogAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Contexts
{
    public class BlogContext : DbContext
    {
       
        public BlogContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Article> Articles { get; set; }
        DbSet<Category>  Categories { get; set; }





    }
}
