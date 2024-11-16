using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.CreateDepot;

internal sealed class CreateDepotCommandHandler(
    IDepotRepository depotRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateDepotCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateDepotCommand request, CancellationToken cancellationToken)
    {
        bool isDepotNameExist = await depotRepository.AnyAsync(d => d.Name == request.Name, cancellationToken);
        if (isDepotNameExist)
        {
            return Result<string>.Failure("Bu isimde bir depo zaten mevcut");
        }

        Depot depo = mapper.Map<Depot>(request);

        await depotRepository.AddAsync(depo, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Depo başarıyla eklendi!";
    }
}
