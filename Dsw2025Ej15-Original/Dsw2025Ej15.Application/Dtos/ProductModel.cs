namespace Dsw2025Ej15.Application.Dtos;

public record ProductModel
{
    public record Request(string Sku, string Name, decimal Price, Guid SubCategoryId);

    public record Response(Guid Id, string? Sku, string? Name, decimal Price, 
        string? SubCategory, string? Category);
}
