using System;
using System.Collections.Generic;

namespace LAB_1.Models;

public partial class Delivery
{
    public int CodeDelivery { get; set; }

    public string NameDelivery { get; set; } = null!;

    public string NameCompany { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Inn { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
