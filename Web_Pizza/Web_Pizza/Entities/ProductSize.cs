using System;
using System.Collections.Generic;

namespace Web_Pizza.Entities;

public partial class ProductSize
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public long SizeId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Size Size { get; set; } = null!;
}
