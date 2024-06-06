
using BlogAPI.Application.Abstraction;
using BlogAPI.Application.Abstraction.Storage;
using BlogAPI.Application.Abstraction.Token;
using BlogAPI.Insfrastructure.Enums;
using BlogAPI.Insfrastructure.Services;
using BlogAPI.Insfrastructure.Services.Storage;
using BlogAPI.Insfrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Insfrastructure
{
    static public class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler,TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage , IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection services, StorageTypes storageTypes)
        {
            switch (storageTypes)
            {
                case StorageTypes.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageTypes.AWS:

                    break;
                case StorageTypes.Azure:

                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }

    }
}
