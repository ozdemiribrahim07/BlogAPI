using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.RemoveArticle
{
    public class RemoveArticleCommandRequest : IRequest<RemoveArticleCommandResponse>
    {
        public string Id { get; set; }
    }
}
