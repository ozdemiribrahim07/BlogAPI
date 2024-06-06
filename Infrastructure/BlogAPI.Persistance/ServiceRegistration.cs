
using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Application.Repositories.CategoryRepo;
using BlogAPI.Application.Repositories.FileBaseRepo;
using BlogAPI.Domain.Entities;
using BlogAPI.Persistance.Contexts;
using BlogAPI.Persistance.Repositories.ArticleImageFileRepo;
using BlogAPI.Persistance.Repositories.ArticleRepo;
using BlogAPI.Persistance.Repositories.CategoryRepo;
using BlogAPI.Persistance.Repositories.FileBaseRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<BlogContext>(opt => opt.UseNpgsql(Configuration.ConnectionString));


            serviceCollection.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<BlogContext>();



            serviceCollection.AddScoped<IArticleReadRepository, ArticleReadRepository>();
            serviceCollection.AddScoped<IArticleWriteRepository, ArticleWriteRepository>();

            serviceCollection.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            serviceCollection.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            serviceCollection.AddScoped<IFileBaseReadRepository, FileBaseReadRepository>();
            serviceCollection.AddScoped<IFileBaseWriteRepository, FileBaseWriteRepository>();

            serviceCollection.AddScoped<IArticleImageFileReadRepository, ArticleImageFileReadRepository>();
            serviceCollection.AddScoped<IArticleImageFileWriteRepository, ArticleImageFileWriteRepo>();

        }

      


    }
}
