using FluentValidation;

namespace Application.Features.Tags.Create
{
    public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        public CreateTagCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Tag ismi boş olamaz")
                                .MaximumLength(50).WithMessage("Tag ismi en fazla 50 karakter olabilir");
        }
    }
}
