using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.ArticleImageFile.RemoveArticleImage
{
    public class RemoveArticleImageCommandRequest : IRequest<RemoveArticleImageCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }

    }
}
