using System;
using System.Collections.Generic;

namespace LAB_1.Models;

public partial class Purchase
{
    public int CodePurchase { get; set; }

    public int CodeBook { get; set; }

    public DateTime DateOrder { get; set; }

    public int CodeDelivery { get; set; }

    public string? TypePurchase { get; set; }

    public decimal Cost { get; set; }

    public int Amount { get; set; }

    public virtual Book CodeBookNavigation { get; set; } = null!;

    public virtual Delivery CodeDeliveryNavigation { get; set; } = null!;
}
