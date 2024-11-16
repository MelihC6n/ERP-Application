using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using FluentValidation;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.UpdateCustomer;
public sealed record UpdateCustomerCommand(
    Guid Id,
    string Name,
    string TaxDepartment,
    string TaxNumber,
    string City,
    string Town,
    string FullAddress) : IRequest<Result<string>>;

internal sealed class UpdateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {

        Customer customer = await customerRepository.GetByExpressionWithTrackingAsync(c => c.Id == request.Id, cancellationToken);

        if (customer == null)
        {
            return Result<string>.Failure("Müşteri bulunamadı!");
        }

        if (customer.TaxNumber != request.TaxNumber)
        {
            bool isTaxNumberExist = await customerRepository.AnyAsync(c => c.TaxNumber == request.TaxNumber, cancellationToken);

            if (isTaxNumberExist)
            {
                return Result<string>.Failure("Bu vergi numarası zaten kullanılıyor!");
            }
        }
        mapper.Map(request, customer);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Müşteri bilgileri başarıyla güncellendi";
    }
}

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
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