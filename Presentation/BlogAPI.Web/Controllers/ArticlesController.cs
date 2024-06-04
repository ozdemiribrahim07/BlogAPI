using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Application.Repositories.FileBaseRepo;
using BlogAPI.Application.RequestParameters;
using BlogAPI.Application.Services;
using BlogAPI.Application.VMs.Articles;
using BlogAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFileService _fileService;

        readonly IFileBaseWriteRepository _fileBaseWriteRepository;
        readonly IFileBaseReadRepository _fileBaseReadRepository;
        readonly IArticleImageFileReadRepository _articleImageFileReadRepository;
        readonly IArticleImageFileWriteRepository _articleImageFileWriteRepository;


        public ArticlesController(IArticleReadRepository articleReadRepository, IArticleWriteRepository articleWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileBaseWriteRepository fileBaseWriteRepository, IFileBaseReadRepository fileBaseReadRepository, IArticleImageFileReadRepository articleImageFileReadRepository, IArticleImageFileWriteRepository articleImageFileWriteRepository)
        {
            _articleReadRepository = articleReadRepository;
            _articleWriteRepository = articleWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _fileBaseWriteRepository = fileBaseWriteRepository;
            _fileBaseReadRepository = fileBaseReadRepository;
            _articleImageFileReadRepository = articleImageFileReadRepository;
            _articleImageFileWriteRepository = articleImageFileWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            var total  = _articleReadRepository.GetAll().Count();
            var articles = _articleReadRepository.GetAll().Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(x => new
            {
                x.Id,
                x.Title,
                x.Content,
                x.CreatedTime,
                x.UpdatedTime
            }).ToList();

            return Ok(new
            {
                articles,
                total
            });
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
        public async Task<IActionResult> Add(ArticleAddVM articleAddVM)
        {
            await _articleWriteRepository.AddAsync(new()
            {
                Title = articleAddVM.Title,
                Content = articleAddVM.Content
            });
            await _articleWriteRepository.SaveAsync();
            return StatusCode((int) HttpStatusCode.Created);
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
        public async Task<IActionResult> Upload()
        {
            var datas = await _fileService.UploadAsync("images", Request.Form.Files);
            await _articleImageFileWriteRepository.AddRangeAsync(datas.Select(x => new ArticleImageFile()
            {
                    FileName = x.fileName,
                    Path = x.path
            }).ToList());
            await _articleImageFileWriteRepository.SaveAsync();
            return Ok();
        }

    }
}
