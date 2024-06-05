using BlogAPI.Application.Abstraction;
using BlogAPI.Application.Features.Articles.Commands.AddArticle;
using BlogAPI.Application.Features.Articles.Commands.ArticleImageFile.RemoveArticleImage;
using BlogAPI.Application.Features.Articles.Commands.ArticleImageFile.UploadArticleImage;
using BlogAPI.Application.Features.Articles.Commands.RemoveArticle;
using BlogAPI.Application.Features.Articles.Commands.UpdateArticle;
using BlogAPI.Application.Features.Articles.Queries.ArticleImageFile.GetArticleImages;
using BlogAPI.Application.Features.Articles.Queries.GetAllArticles;
using BlogAPI.Application.Features.Articles.Queries.GetByIdArticles;
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
using static System.Net.Mime.MediaTypeNames;

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


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdArticleQueryRequest getByIdArticleQueryRequest)
        {
            GetByIdArticleQueryResponse getByIdArticleQueryResponse = await _mediator.Send(getByIdArticleQueryRequest);
            return Ok(getByIdArticleQueryResponse);
        }

     

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Remove([FromRoute] RemoveArticleCommandRequest removeArticleCommandRequest)
        {
           RemoveArticleCommandResponse removeArticleCommandResponse = await _mediator.Send(removeArticleCommandRequest);
            return Ok(removeArticleCommandResponse);
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddArticleCommandRequest addArticleCommandRequest)
        {
           AddArticleCommandResponse response = await _mediator.Send(addArticleCommandRequest);
           return Ok(response);
        }



        [HttpPut]
        public async Task<IActionResult> Update([FromBody]UpdateArticleCommandRequest updateArticleCommandRequest)
        {
            UpdateArticleCommandResponse response = await _mediator.Send(updateArticleCommandRequest);
            return Ok(response);
        }
         


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadArticleImageCommandRequest uploadArticleImageCommandRequest)
        {
            uploadArticleImageCommandRequest.Files = Request.Form.Files;
            UploadArticleImageCommandResponse response = await _mediator.Send(uploadArticleImageCommandRequest);
            return Ok(response);
        }


        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetImages([FromRoute]GetArticleImagesQueryRequest getArticleImagesQueryRequest)
        {
           List<GetArticleImagesQueryResponse> getAllArticlesQueryResponse = await _mediator.Send(getArticleImagesQueryRequest);
            return Ok(getAllArticlesQueryResponse);
        }


        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> RemoveImage([FromRoute]RemoveArticleImageCommandRequest removeArticleImageCommandRequest, [FromQuery] string imageId)
        {
            removeArticleImageCommandRequest.ImageId = imageId;

            RemoveArticleImageCommandResponse removeArticleImageCommandResponse = await _mediator.Send(removeArticleImageCommandRequest);
            return Ok(removeArticleImageCommandResponse);
        }



    }
}
