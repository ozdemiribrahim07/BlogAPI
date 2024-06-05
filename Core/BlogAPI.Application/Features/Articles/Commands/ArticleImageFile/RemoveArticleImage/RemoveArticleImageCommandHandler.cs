using BlogAPI.Application.Repositories.ArticleImageFileRepo;
using BlogAPI.Application.Repositories.ArticleRepo;
using BlogAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogAPI.Application.Features.Articles.Commands.ArticleImageFile.RemoveArticleImage
{
    public class RemoveArticleImageCommandHandler : IRequestHandler<RemoveArticleImageCommandRequest, RemoveArticleImageCommandResponse>
    {
        readonly IArticleReadRepository _articleReadRepository;
        readonly IArticleImageFileWriteRepository _articleImageFileWriteRepository;
        public RemoveArticleImageCommandHandler(IArticleReadRepository articleReadRepository, IArticleImageFileWriteRepository articleImageFileWriteRepository)
        {
            _articleReadRepository = articleReadRepository;
            _articleImageFileWriteRepository = articleImageFileWriteRepository;
        }

        public async Task<RemoveArticleImageCommandResponse> Handle(RemoveArticleImageCommandRequest request, CancellationToken cancellationToken)
        {
            Article? article = await _articleReadRepository.Table.Include(x => x.ArticleImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

            Domain.Entities.ArticleImageFile? articleImageFile = article?.ArticleImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(request.ImageId));

            if (articleImageFile != null)
            {
                article?.ArticleImageFiles.Remove(articleImageFile);
            }
            await _articleImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
