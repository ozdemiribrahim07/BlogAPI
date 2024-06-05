using BlogAPI.Application.Abstraction;
using BlogAPI.Application.Features.Articles.Commands.AddArticle;
using BlogAPI.Application.Features.Articles.Queries.GetAllArticles;
using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Application.Repositories.FileBaseRepo;
using BlogAPI.Application.RequestParameters;
using BlogAPI.Application.VMs.Articles;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BlogAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleReadRepository _articleReadRepository;
        private readonly IArticleWriteRepository _articleWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IStorageService _storageService;
        readonly IConfiguration _configuration;


        readonly IMediator _mediator;
        

        readonly IFileBaseWriteRepository _fileBaseWriteRepository;
        readonly IFileBaseReadRepository _fileBaseReadRepository;
        readonly IArticleImageFileReadRepository _articleImageFileReadRepository;
        readonly IArticleImageFileWriteRepository _articleImageFileWriteRepository;


        public ArticlesController(IArticleReadRepository articleReadRepository, IArticleWriteRepository articleWriteRepository, IWebHostEnvironment webHostEnvironment, IFileBaseWriteRepository fileBaseWriteRepository, IFileBaseReadRepository fileBaseReadRepository, IArticleImageFileReadRepository articleImageFileReadRepository, IArticleImageFileWriteRepository articleImageFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _articleReadRepository = articleReadRepository;
            _articleWriteRepository = articleWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileBaseWriteRepository = fileBaseWriteRepository;
            _fileBaseReadRepository = fileBaseReadRepository;
            _articleImageFileReadRepository = articleImageFileReadRepository;
            _articleImageFileWriteRepository = articleImageFileWriteRepository;
            _storageService = storageService;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllArticlesQueryRequest getAllArticlesQueryRequest)
        {
           GetAllArticlesQueryResponse getAllArticlesQueryResponse = await _mediator.Send(getAllArticlesQueryRequest);
            return Ok(getAllArticlesQueryResponse);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _articleReadRepository.GetByIdAsync(id));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(string id)
        {
            await _articleWriteRepository.RemoveAsync(id);
            await _articleWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddArticleCommandRequest addArticleCommandRequest)
        {
           AddArticleCommandResponse response = await _mediator.Send(addArticleCommandRequest);
           return Ok(response);
        }



        [HttpPut]
        public async Task<IActionResult> Update(ArticleUpdateVM articleUpdateVM)
        {
            Article article = await _articleReadRepository.GetByIdAsync(articleUpdateVM.Id);

            article.Title = articleUpdateVM.Title;
            article.Content = articleUpdateVM.Content;

            await _articleWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("images", Request.Form.Files, _webHostEnvironment);

            Article article = await _articleReadRepository.GetByIdAsync(id);

            await _articleImageFileWriteRepository.AddRangeAsync(result.Select(x => new ArticleImageFile()
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = "Local",
                Articles = new List<Article>() { article }
            }).ToList());

            await _articleImageFileWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetImages(string id)
        {
           Article? article = await _articleReadRepository.Table.Include(x => x.ArticleImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            return Ok(article.ArticleImageFiles.Select(x => new {
               Path = $"{ _configuration["LocalStorageUrl"]}/{x.Path}/{x.FileName}",
               x.FileName,
               x.Id
            }));
        }


        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> RemoveImage(string id, string imageId)
        {
            Article? article = await _articleReadRepository.Table.Include(x => x.ArticleImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            ArticleImageFile articleImageFile = article.ArticleImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(imageId));

            article.ArticleImageFiles.Remove(articleImageFile);
            await _articleImageFileWriteRepository.SaveAsync();
            return Ok();
        }



    }
}
