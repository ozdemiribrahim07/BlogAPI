using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.ArticleImageFile.GetArticleImages
{
    public class GetArticleImagesQueryRequest : IRequest<List<GetArticleImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
