using BlogAPI.Application.Repositories;
using BlogAPI.Domain.Entities.Common;
using BlogAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly BlogContext _blogContext;

        public ReadRepository(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public DbSet<T> Table => _blogContext.Set<T>();

        public IQueryable<T> GetAll() =>
            Table;
       

        public async Task<T> GetAsync(Expression<Func<T, bool>> method)
            => await Table.FirstOrDefaultAsync(method);


        public async Task<T> GetByIdAsync(string id)
            => await Table.FindAsync(Guid.Parse(id));


        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
            => Table.Where(method);


    }
}
