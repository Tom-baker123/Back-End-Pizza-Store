using System;
using System.Collections.Generic;

namespace Web_Pizza.Entities;

public partial class Topping
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<OrderTopping> OrderToppings { get; set; } = new List<OrderTopping>();
}
