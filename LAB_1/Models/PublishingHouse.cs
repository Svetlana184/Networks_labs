using System;
using System.Collections.Generic;

namespace LAB_1.Models;

public partial class PublishingHouse
{
    public int CodePublish { get; set; }

    public string Publish { get; set; } = null!;

    public string? City { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
