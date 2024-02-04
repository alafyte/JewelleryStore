using System;
using System.Collections.Generic;

namespace JewelleryStore;

public partial class Bill
{
    public int BillId { get; set; }

    public DateTime DateOfOrder { get; set; }

    public decimal TotalPrice { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<OrderInfo> OrderInfos { get; } = new List<OrderInfo>();

    public virtual User User { get; set; } = null!;
}
