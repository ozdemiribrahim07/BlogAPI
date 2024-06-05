using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.AddArticle
{
    public class AddArticleCommandRequest : IRequest<AddArticleCommandResponse>
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
