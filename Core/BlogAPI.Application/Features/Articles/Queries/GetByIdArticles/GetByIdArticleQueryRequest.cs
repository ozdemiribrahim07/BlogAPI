using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Queries.GetByIdArticles
{
    public class GetByIdArticleQueryRequest : IRequest<GetByIdArticleQueryResponse>
    {
        public string Id { get; set; }

    }
}
