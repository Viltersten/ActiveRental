using FluentValidation;

namespace Api.Auxiliaries.Validators.Dtos;

public class ReturnDtoValidator : AbstractValidator<ReturnDto>
{
    public ReturnDtoValidator()
    {
        RuleFor(x => x.Occasion)
            .Must(x => x < DateTime.Now)
            .WithMessage("Must not register posterior return.");
    }
}