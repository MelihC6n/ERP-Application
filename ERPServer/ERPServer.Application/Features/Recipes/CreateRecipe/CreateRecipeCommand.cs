using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipes.CreateRecipe;
public sealed record CreateRecipeCommand(
    Guid ProductId,
    List<RecipeDetailDto> RecipeDetails) : IRequest<Result<string>>;
