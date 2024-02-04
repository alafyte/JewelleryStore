using System;
using System.Collections.Generic;

namespace JewelleryStore;

public partial class Metal
{
    public int MetalId { get; set; }

    public string MetalNameRus { get; set; } = null!;

    public string MetalNameEn { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
