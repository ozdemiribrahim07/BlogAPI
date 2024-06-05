using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.GetAllArticles
{
    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQueryRequest, GetAllArticlesQueryResponse>
    {
        private readonly IArticleReadRepository _articleReadRepository;

        public GetAllArticlesQueryHandler(IArticleReadRepository articleReadRepository)
        {
            _articleReadRepository = articleReadRepository;
        }

        public Task<GetAllArticlesQueryResponse> Handle(GetAllArticlesQueryRequest request, CancellationToken cancellationToken)
        {
            var total = _articleReadRepository.GetAll().Count();
            var articles = _articleReadRepository.GetAll().Skip(request.Page * request.Size).Take(request.Size).Select(x => new
            {
                x.Id,
                x.Title,
                x.Content,
                x.CreatedTime,
                x.UpdatedTime
            }).ToList();

            var response = new GetAllArticlesQueryResponse
            {
                Articles = articles,
                TotalCount = total
            };
            return Task.FromResult(response);

        }
    }
}
