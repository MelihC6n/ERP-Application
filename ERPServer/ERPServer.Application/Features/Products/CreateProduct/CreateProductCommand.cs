namespace ERPServer.Application.Features.Products.CreateProduct;
public sealed record CreateProductCommand(
    string Name,
    int TypeValue)