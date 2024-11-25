using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipes.DeleteRecipe;
public sealed record DeleteRecipeCommand(
    Guid Id) : IRequest<Result<string>>;
