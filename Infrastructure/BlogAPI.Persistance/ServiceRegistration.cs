
using BlogAPI.Persistance.Contexts;
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
            serviceCollection.AddDbContext<BlogContext>(opt => opt.UseNpgsql("Server=127.0.0.1;Port=5432;Database=BlogDb;User Id=postgres;Password=1289;"));

        }


    }
}
