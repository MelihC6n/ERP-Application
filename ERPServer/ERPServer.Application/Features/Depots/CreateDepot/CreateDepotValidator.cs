using FluentValidation;

namespace ERPServer.Application.Features.Depots.CreateDepot;

public sealed class CreateDepotValidator : AbstractValidator<CreateDepotCommand>
{
    public CreateDepotValidator()
    {
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olabilir.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Şehir alanı en fazla 50 karakter olabilir.");

            RuleFor(x => x.Town)
                .NotEmpty().WithMessage("İlçe alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("İlçe alanı en fazla 50 karakter olabilir.");

            RuleFor(x => x.FullAddress)
                .NotEmpty().WithMessage("Tam Adres alanı boş bırakılamaz.")
                .MaximumLength(250).WithMessage("Tam Adres alanı en fazla 250 karakter olabilir.");
        }
    }
}