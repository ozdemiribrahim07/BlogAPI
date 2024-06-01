using BlogAPI.Application.Repositories.ArticleRepo;
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
            return Ok("naber");

        }
    }
}
