using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.UpdateDepot;

internal sealed class UpdateDepotCommandHandler(
    IDepotRepository depotRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateDepotCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDepotCommand request, CancellationToken cancellationToken)
    {
        Depot depot = await depotRepository.GetByExpressionWithTrackingAsync(d => d.Id == request.Id, cancellationToken);
        if (depot == null)
        {
            return Result<string>.Failure("Depo bulunamadı!");
        }

        if (depot.Name != request.Name)
        {
            bool isDepotNameExist = await depotRepository.AnyAsync(d => d.Name == request.Name, cancellationToken);
            if (isDepotNameExist)
            {
                return Result<string>.Failure("Bu isimde bir depo zaten mevcut");
            }
        }

        mapper.Map(request, depot);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Depo başarıyla güncellendi.";
    }
}
