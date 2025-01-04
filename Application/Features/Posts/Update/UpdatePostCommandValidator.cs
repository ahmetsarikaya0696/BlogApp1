using FluentValidation;

namespace Application.Features.Posts.Update
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş olamaz")
                                 .MaximumLength(250).WithMessage("Başlık en fazla 250 karakter olabilir");

            RuleFor(x => x.Content).NotEmpty().WithMessage("İçerik alanı boş olamaz");
        }
    }
}
