using Dsw2025Ej15.Application.Dtos;
using Dsw2025Ej15.Application.Exceptions;
using Dsw2025Ej15.Domain;
using Dsw2025Ej15.Domain.Entities;

namespace Dsw2025Ej15.Application.Services;

public class ProductsManagementService
{
    private readonly IRepository _repository;

    public ProductsManagementService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductModel.Response?> GetProductById(Guid id)
    {
        var product = await _repository.GetById<Product>(id);
        return product != null ?
            new ProductModel.Response(product.Id, product.Sku, product.Name, product.CurrentUnitPrice, product.SubCategory?.Name, 
            product.SubCategory?.Category?.Name) :
            null;
    }

    public async Task<IEnumerable<ProductModel.Response>?> GetProducts()
    {
        return (await _repository
            .GetFiltered<Product>(p => p.IsActive, "SubCategory.Category"))?
            .Select(p => new ProductModel.Response(p.Id, p.Sku, p.Name, 
            p.CurrentUnitPrice, p.SubCategory?.Name, p.SubCategory?.Category?.Name));
    }

    public async Task<ProductModel.Response> AddProduct(ProductModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Sku) || 
            string.IsNullOrWhiteSpace(request.Name) ||
            request.Price < 0 || request.SubCategoryId == Guid.Empty
            )
        {
            throw new ArgumentException("Valores para el producto no válidos");
        }

        var exist = await _repository.First<Product>(p => p.Sku == request.Sku);
        if (exist != null) throw new DuplicatedEntityException($"Ya existe un producto con el Sku {request.Sku}");
        var category = await _repository.GetById<SubCategory>(request.SubCategoryId) ?? throw new EntityNotFoundException($"La categoría o rubro indicado no existe");
        var product = new Product(request.Sku, request.Name, request.Price, request.SubCategoryId);
        await _repository.Add(product);
        return new ProductModel.Response(product.Id, product.Sku, product.Name,
            product.CurrentUnitPrice, product.SubCategory?.Name, product.SubCategory?.Category?.Name);
    }
}
