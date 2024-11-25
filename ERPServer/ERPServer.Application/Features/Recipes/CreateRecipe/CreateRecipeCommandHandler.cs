using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipes.CreateRecipe;

internal sealed class CreateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateRecipeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        bool isRecipeExist = await recipeRepository.AnyAsync(r => r.ProductId == request.ProductId, cancellationToken);
        if (isRecipeExist)
        {
            return Result<string>.Failure("Bu ürüne ait reçete zaten mevcut!");
        }
        Recipe recipe = new()
        {
            ProductId = request.ProductId,
            RecipeDetails = request.RecipeDetails.Select(rd => new RecipeDetail()
            {
                ProductId = rd.ProductId,
                Quantity = rd.Quantity
            }).ToList()
        };

        await recipeRepository.AddAsync(recipe, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Tarif başarıyla eklendi!";
    }
}
