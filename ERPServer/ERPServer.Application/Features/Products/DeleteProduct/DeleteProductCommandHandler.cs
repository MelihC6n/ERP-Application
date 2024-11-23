using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.DeleteProduct;

internal sealed class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionAsync(x => x.Id == request.Id, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("Ürün bulunamadı!");
        }

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün başarıyla silindi.";
    }
}
