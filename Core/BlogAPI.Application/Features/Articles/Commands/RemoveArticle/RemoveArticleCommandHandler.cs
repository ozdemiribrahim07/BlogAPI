using BlogAPI.Application.Repositories.ArticleRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.RemoveArticle
{
    public class RemoveArticleCommandHandler : IRequestHandler<RemoveArticleCommandRequest, RemoveArticleCommandResponse>
    {
        readonly IArticleWriteRepository _articleWriteRepository;

        public RemoveArticleCommandHandler(IArticleWriteRepository articleWriteRepository)
        {
            _articleWriteRepository = articleWriteRepository;
        }

        public async Task<RemoveArticleCommandResponse> Handle(RemoveArticleCommandRequest request, CancellationToken cancellationToken)
        {
            await _articleWriteRepository.RemoveAsync(request.Id);
            await _articleWriteRepository.SaveAsync();

            return new();
        }
    }
}
