using BlogAPI.Application.Repositories.ArticleRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.AddArticle
{
    public class AddArticleCommandHandler : IRequestHandler<AddArticleCommandRequest, AddArticleCommandResponse>
    {
        readonly IArticleWriteRepository _articleWriteRepository;

        public AddArticleCommandHandler(IArticleWriteRepository articleWriteRepository)
        {
            _articleWriteRepository = articleWriteRepository;
        }

        public async Task<AddArticleCommandResponse> Handle(AddArticleCommandRequest request, CancellationToken cancellationToken)
        {

            await _articleWriteRepository.AddAsync(new()
            {
                Title = request.Title,
                Content = request.Content
            });

            await _articleWriteRepository.SaveAsync();
            return new();

        }
    }
}
