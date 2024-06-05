using BlogAPI.Application.Abstraction;
using BlogAPI.Application.Abstraction.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Insfrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }
        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files, IWebHostEnvironment? webHostEnvironment)
           => _storage.UploadAsync(pathOrContainerName, files, webHostEnvironment);


        public async Task DeleteAsync(string fileName, string pathOrContainerName)
            => await _storage.DeleteAsync(pathOrContainerName, fileName);



        public List<string> GetFiles(string pathOrContainerName)
            =>  _storage.GetFiles(pathOrContainerName);



        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);



       


    }

}
