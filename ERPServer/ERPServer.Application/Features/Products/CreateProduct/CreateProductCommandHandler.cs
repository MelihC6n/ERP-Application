using Ardalis.SmartEnum;
using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductTypeEnum productType = SmartEnum<ProductTypeEnum>.FromValue(request.TypeValue);
        if (productType is null)
        {
            return Result<string>.Failure("Lütfen ürün tipi seçiniz!");
        }

        bool isProductExist = await productRepository.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (isProductExist)
        {
            return Result<string>.Failure("Bu ürün zaten mevcut lütfen ürün ismini kontrol edin!");
        }

        Product product = mapper.Map<Product>(request);

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Ürün başarıyla eklendi.";
    }
}
