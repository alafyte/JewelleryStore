using System;
using System.Collections.Generic;

namespace JewelleryStore;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string Ptype { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
