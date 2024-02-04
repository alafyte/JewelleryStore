using System;
using System.Collections.Generic;

namespace JewelleryStore;

public partial class OrderInfo
{
    public int ProductCode { get; set; }

    public int BillId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Product ProductCodeNavigation { get; set; } = null!;

}
