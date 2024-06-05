using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.ArticleImageFile.GetArticleImages
{
    public class GetArticleImagesQueryHandler : IRequestHandler<GetArticleImagesQueryRequest, List<GetArticleImagesQueryResponse>>
    {

        readonly IArticleReadRepository _articleReadRepository;
        readonly IConfiguration _configuration;

        public GetArticleImagesQueryHandler(IArticleReadRepository articleReadRepository, IConfiguration configuration)
        {
            _articleReadRepository = articleReadRepository;
            _configuration = configuration;
        }


        public async Task<List<GetArticleImagesQueryResponse>> Handle(GetArticleImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Article? article = await _articleReadRepository.Table.Include(x => x.ArticleImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

            return article.ArticleImageFiles.Select(x => new GetArticleImagesQueryResponse
            {
                Path  =  $"{_configuration["LocalStorageUrl"]}/{x.Path}/{x.FileName}",
                FileName = x.FileName,
                Id = x.Id
            }).ToList();


            
        }
    }
}
