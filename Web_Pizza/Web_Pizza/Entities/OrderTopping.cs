using System;
using System.Collections.Generic;

namespace Web_Pizza.Entities;

public partial class OrderTopping
{
    public long Id { get; set; }

    public long OrderDetailId { get; set; }

    public long ToppingId { get; set; }

    public decimal Price { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;

    public virtual Topping Topping { get; set; } = null!;
}
