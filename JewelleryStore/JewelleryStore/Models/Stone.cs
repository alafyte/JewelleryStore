using System;
using System.Collections.Generic;

namespace JewelleryStore;

public partial class Stone
{
    public int StoneId { get; set; }

    public string StoneNameRus { get; set; } = null!;

    public string StoneNameEn { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
