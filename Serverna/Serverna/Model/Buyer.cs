using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class Buyer
{
    public int IdBuyer { get; set; }

    public string? Name { get; set; }

    public string? ContactPhone { get; set; }

    public string? ContactEmail { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
