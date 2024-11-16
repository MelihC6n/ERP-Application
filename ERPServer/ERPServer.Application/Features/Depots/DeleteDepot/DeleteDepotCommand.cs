using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.DeleteDepot;
public sealed record DeleteDepotCommand(
    Guid Id) : IRequest<Result<string>>;
