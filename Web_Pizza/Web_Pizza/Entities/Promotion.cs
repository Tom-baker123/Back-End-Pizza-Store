using System;
using System.Collections.Generic;

namespace Web_Pizza.Entities;

public partial class Promotion
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Discount { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
