using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipes.DeleteRecipe;

internal sealed class DeleteRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteRecipeCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        Recipe recipe = await recipeRepository.GetByExpressionAsync(r => r.Id == request.Id, cancellationToken);

        if (recipe == null)
        {
            return Result<string>.Failure("Reçete bulunamadı!");
        }

        recipeRepository.Delete(recipe);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Reçete başarıyla silindi.";
    }
}
