using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Domain.Entities;
using BlogAPI.Web.Controllers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Features.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommandRequest, UpdateArticleCommandResponse>
    {
        readonly IArticleReadRepository _articleReadRepository;
        readonly IArticleWriteRepository _articleWriteRepository;

        public UpdateArticleCommandHandler(IArticleReadRepository articleReadRepository, IArticleWriteRepository articleWriteRepository)
        {
            _articleReadRepository = articleReadRepository;
            _articleWriteRepository = articleWriteRepository;
        }

        public async Task<UpdateArticleCommandResponse> Handle(UpdateArticleCommandRequest request, CancellationToken cancellationToken)
        {
            Article article = await _articleReadRepository.GetByIdAsync(request.Id);

            article.Title = request.Title;
            article.Content = request.Content;

            await _articleWriteRepository.SaveAsync();

            return new UpdateArticleCommandResponse();
        }
    }
}
