using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public string? ContactPerson { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
