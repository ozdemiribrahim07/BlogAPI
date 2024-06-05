using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.GetByIdArticles
{
    public class GetByIdArticlesQueryHandler : IRequestHandler<GetByIdArticleQueryRequest, GetByIdArticleQueryResponse>
    {
        readonly IArticleReadRepository _articleReadRepository;

        public GetByIdArticlesQueryHandler(IArticleReadRepository articleReadRepository)
        {
            _articleReadRepository = articleReadRepository;
        }

        public async Task<GetByIdArticleQueryResponse> Handle(GetByIdArticleQueryRequest request, CancellationToken cancellationToken)
        {
            Article article = await _articleReadRepository.GetByIdAsync(request.Id);
               
            return new GetByIdArticleQueryResponse
            {
                Title = article.Title,
                Content = article.Content
            };
        }
    }
}
