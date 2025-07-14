using System.Reflection.Metadata;

namespace Dsw2025Ej15.Domain.Entities;

public class Product: EntityBase
{
    public Product()
    {
        
    }
    public Product(string sku, string name, decimal price, Guid subCategoryId)
    {
        Sku = sku;
        Name = name;
        CurrentUnitPrice = price;
        IsActive = true;
        SubCategoryId = subCategoryId;
    }
    public string? Sku { get; set; }
    public string? Name { get; set; }
    public decimal CurrentUnitPrice { get; set; }
    public bool  IsActive { get; set; }

    public Guid? SubCategoryId { get; set; }
    public SubCategory? SubCategory { get; set; }
}
