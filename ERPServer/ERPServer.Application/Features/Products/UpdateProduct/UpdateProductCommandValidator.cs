using FluentValidation;

namespace ERPServer.Application.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Ürün adı boş olamaz.")
            .MaximumLength(100)
            .WithMessage("Ürün adı 100 karakterden uzun olamaz.");

        RuleFor(command => command.TypeValue)
            .GreaterThan(0)
            .WithMessage("Tür değeri boş olamaz.");
    }
}