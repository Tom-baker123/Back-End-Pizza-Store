using System;
using System.Collections.Generic;

namespace Web_Pizza.Entities;

public partial class Size
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal PriceMultiplier { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
}
