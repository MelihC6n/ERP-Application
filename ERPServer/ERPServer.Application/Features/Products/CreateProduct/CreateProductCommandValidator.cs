using FluentValidation;

namespace ERPServer.Application.Features.Products.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Ürün adı boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Ürün adı 100 karakterden uzun olamaz.");

        RuleFor(command => command.TypeValue)
            .GreaterThan(0)
            .WithMessage("Tür değeri 0'dan büyük olmalıdır.");
    }
}