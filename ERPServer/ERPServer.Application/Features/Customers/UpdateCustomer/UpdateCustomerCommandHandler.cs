using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.UpdateCustomer;

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
