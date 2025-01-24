using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web_Pizza.Entities;

public partial class User
{
    [JsonIgnore]
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public string ConfirmationToken { get; set; } = null!;

    public DateTime? TokenExpiration { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
