using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Application.VMs.Articles;
using BlogAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleReadRepository _articleReadRepository;
        private readonly IArticleWriteRepository _articleWriteRepository;

        public ArticlesController(IArticleReadRepository articleReadRepository, IArticleWriteRepository articleWriteRepository)
        {
            _articleReadRepository = articleReadRepository;
            _articleWriteRepository = articleWriteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_articleReadRepository.GetAll());
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
            return Ok();
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

    }
}
