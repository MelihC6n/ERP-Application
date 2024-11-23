using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByExpressionWithTrackingAsync(x => x.Id == request.Id, cancellationToken);
        if (product == null)
        {
            return Result<string>.Failure("Ürün bulunamadı!");
        }

        if (product.Name != request.Name)
        {
            bool isNameExist = await productRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
            if (isNameExist)
            {
                return Result<string>.Failure("Bu ürün ismi zaten kullanılıyor, lütfen farklı bir isim giriniz!");
            }
        }

        mapper.Map(request, product);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Ürün başarıyla güncellendi";

    }
}
