using BlogAPI.Domain.Entities;
using BlogAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.Entries<BaseEntity>()
                .ToList()
                .ForEach
                (x => {
                    x.Entity.UpdatedTime = DateTime.UtcNow;
                    x.Entity.CreatedTime = DateTime.UtcNow;
                    _ = true;
                       });

            return await base.SaveChangesAsync(cancellationToken);

        }



    }
}
