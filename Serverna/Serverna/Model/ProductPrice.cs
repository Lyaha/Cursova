using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class ProductPrice
{
    public int PriceId { get; set; }

    public int? ProductId { get; set; }

    public decimal? Price { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? Quantity { get; set; }

    public string? BatchNumber { get; set; }

    public virtual ICollection<PositionProduct> PositionProducts { get; set; } = new List<PositionProduct>();

    public virtual Product? Product { get; set; }
}
