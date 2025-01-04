using FluentValidation;

namespace Application.Features.Posts.Create
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("Yazar alanı boş olamaz");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz")
                                 .MaximumLength(250).WithMessage("Başlık en fazla 250 karakter olabilir");

            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz");
        }
    }
}
