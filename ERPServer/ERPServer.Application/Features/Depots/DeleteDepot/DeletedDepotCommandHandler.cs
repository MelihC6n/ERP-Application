using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.DeleteDepot;

internal sealed class DeletedDepotCommandHandler(
    IDepotRepository depotRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepotCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepotCommand request, CancellationToken cancellationToken)
    {
        var depot = await depotRepository.FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (depot is null)
        {
            return Result<string>.Failure("Depo bulunamadı!");
        }

        depotRepository.Delete(depot);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Depo başarıyla silindi.";
    }
}
