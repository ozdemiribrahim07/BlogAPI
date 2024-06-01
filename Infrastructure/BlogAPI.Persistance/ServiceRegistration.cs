﻿
using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Application.Repositories.CategoryRepo;
using BlogAPI.Persistance.Contexts;
using BlogAPI.Persistance.Repositories.ArticleRepo;
using BlogAPI.Persistance.Repositories.CategoryRepo;
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

            serviceCollection.AddScoped<IArticleReadRepository, ArticleReadRepository>();
            serviceCollection.AddScoped<IArticleWriteRepository, ArticleWriteRepository>();

            serviceCollection.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            serviceCollection.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

        }

      


    }
}
