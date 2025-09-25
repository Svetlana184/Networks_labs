using System;
using System.Collections.Generic;

namespace LAB_1.Models;

public partial class Author
{
    public int CodeAuthor { get; set; }

    public string NameAuthor { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
