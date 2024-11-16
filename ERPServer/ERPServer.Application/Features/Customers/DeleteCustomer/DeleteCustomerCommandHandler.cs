using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Customers.DeleteCustomer;

internal sealed class DeleteCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (customer == null)
        {
            return Result<string>.Failure("Müşteri bulunamadı!");
        }

        customerRepository.Delete(customer);
        await unitOfWork.SaveChangesAsync();

        return customer.Name + " isimli müşteri başarıyla silindi.";
    }
}
