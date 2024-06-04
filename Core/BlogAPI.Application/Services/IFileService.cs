﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
        //Task<string> FileRenameAsync(string path, string fileName);
        Task<bool> CopyFileAsync(string path, IFormFile file);
        
    }
}
