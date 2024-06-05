using BlogAPI.Application.Abstraction.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Insfrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task DeleteAsync(string fileName, string pathOrContainerName)
          => File.Delete($"{pathOrContainerName}/{fileName}");


        public bool HasFile(string pathOrContainerName, string fileName)
            => File.Exists($"{pathOrContainerName}/{fileName}");


        public List<string> GetFiles(string pathOrContainerName)
        {
            DirectoryInfo directory = new(pathOrContainerName);
            return directory.GetFiles().Select(file => file.Name).ToList();
        } /*Directory.GetFiles(pathOrContainerName, "*", SearchOption.AllDirectories).ToList();*/
           

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files, IWebHostEnvironment webHostEnvironment)
        {
            string uploadPath = Path.Combine(webHostEnvironment.WebRootPath, pathOrContainerName);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            List<(string fileName, string path)> datas = new List<(string fileName, string path)>();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(file.FileName, uploadPath);

                bool copied = await CopyFileAsync(Path.Combine(uploadPath, fileNewName), file);
                if (copied)
                {
                    datas.Add((fileNewName, $"{pathOrContainerName}/{fileNewName}"));
                }
                else
                {
                   
                }
            }

            return datas;
        }


    }
}
