using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej15.Domain.Entities;

public class SubCategory: EntityBase
{
    public string? Name { get; set; }
    public bool IsActive { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Product> Products { get; } = new HashSet<Product>();
}
