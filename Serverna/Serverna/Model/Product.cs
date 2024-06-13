using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    public int? StockQuantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public virtual Supplier? Supplier { get; set; }
}
