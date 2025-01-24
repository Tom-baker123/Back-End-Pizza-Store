using System;
using System.Collections.Generic;

namespace Web_Pizza.Entities;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public decimal TotalPrice { get; set; }

    public string? Status { get; set; }

    public string? PaymentMethod { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public long? PromotionId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Promotion? Promotion { get; set; }

    public virtual User User { get; set; } = null!;
}
