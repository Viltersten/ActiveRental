using FluentValidation;

namespace Api.Auxiliaries.Validators.Dtos;

public class PickupDtoValidator : AbstractValidator<PickupDto>
{
    public PickupDtoValidator()
    {
        RuleFor(x => x.Plate)
            .Must(x => !Regex.IsMatch(x, Patterns.Plate))
            .WithMessage("Must not register invalid plate number.");

        RuleFor(x => x.CustomerId)
            .Must(x => !x.Is(Patterns.Cid) && !x.Is(Patterns.Guid))
            .WithMessage("Must use SSN, CID or GUID.");
        
        RuleFor(x => x.Occasion)
            .Must(x => x >= DateTime.Today)
            .WithMessage("Must not back-date pickups.");
    }
}