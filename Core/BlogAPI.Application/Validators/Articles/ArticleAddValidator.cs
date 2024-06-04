using BlogAPI.Application.VMs.Articles;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Validators.Articles
{
    public class ArticleAddValidator :AbstractValidator<ArticleAddVM>
    {
        public ArticleAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MinimumLength(2).WithMessage("Başlık Boş Geçilmemeli ve en az 2 karakter olmalıdır..");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("Yazı Boş Geçilmemeli..");

        }


    }
}
