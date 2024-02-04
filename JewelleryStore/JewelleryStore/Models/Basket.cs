using System;
using System.Collections.Generic;

namespace JewelleryStore;

public partial class Basket
{
    public int BasketId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
