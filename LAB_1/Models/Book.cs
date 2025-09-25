using System;
using System.Collections.Generic;

namespace LAB_1.Models;

public partial class Book
{
    public int CodeBook { get; set; }

    public string TitleBook { get; set; } = null!;

    public int CodeAuthor { get; set; }

    public int? Pages { get; set; }

    public int CodePublish { get; set; }

    public virtual Author CodeAuthorNavigation { get; set; } = null!;

    public virtual PublishingHouse CodePublishNavigation { get; set; } = null!;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
