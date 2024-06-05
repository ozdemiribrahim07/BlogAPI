using BlogAPI.Application.Abstraction;
using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.ArticleImageFile.UploadArticleImage
{
    public class UploadArticleImageCommandHandler : IRequestHandler<UploadArticleImageCommandRequest, UploadArticleImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IArticleReadRepository _articleReadRepository;
        readonly IArticleImageFileWriteRepository _articleImageFileWriteRepository;
        readonly IWebHostEnvironment _webHostEnvironment;

        public UploadArticleImageCommandHandler(IArticleImageFileWriteRepository articleImageFileWriteRepository, IArticleReadRepository articleReadRepository, IStorageService storageService, IWebHostEnvironment webHostEnvironment)
        {
            _articleImageFileWriteRepository = articleImageFileWriteRepository;
            _articleReadRepository = articleReadRepository;
            _storageService = storageService;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<UploadArticleImageCommandResponse> Handle(UploadArticleImageCommandRequest request, CancellationToken cancellationToken)
        {

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("images", request.Files, _webHostEnvironment);

            Article article = await _articleReadRepository.GetByIdAsync(request.Id);

            await _articleImageFileWriteRepository.AddRangeAsync(result.Select(x => new Domain.Entities.ArticleImageFile()
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = "Local",
                Articles = new List<Article>() { article }
            }).ToList());

            await _articleImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
