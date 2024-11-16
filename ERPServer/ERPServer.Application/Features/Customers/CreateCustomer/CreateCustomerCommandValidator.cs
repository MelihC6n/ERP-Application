using FluentValidation;

namespace ERPServer.Application.Features.Customers.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
            .MaximumLength(100).WithMessage("Ad alanı en fazla 100 karakter olabilir.");

        RuleFor(x => x.TaxDepartment)
            .NotEmpty().WithMessage("Vergi Dairesi alanı boş bırakılamaz.")
            .MaximumLength(50).WithMessage("Vergi Dairesi alanı en fazla 50 karakter olabilir.");

        RuleFor(x => x.TaxNumber)
            .NotEmpty().WithMessage("Vergi Numarası alanı boş bırakılamaz.")
            .Matches(@"^\d+$").WithMessage("Vergi Numarası yalnızca rakamlardan oluşmalıdır.")
            .Length(10, 11).WithMessage("Vergi Numarası 10 ile 11 karakter arasında olmalıdır.");

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