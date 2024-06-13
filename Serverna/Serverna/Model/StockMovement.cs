using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class StockMovement
{
    public int MovementId { get; set; }

    public int? ProductId { get; set; }

    public int? MovementTypeId { get; set; }

    public int? Quantity { get; set; }

    public DateTime? MovementDate { get; set; }

    public string? BatchNumber { get; set; }

    public string? Notes { get; set; }

    public virtual MovementType? MovementType { get; set; }

    public virtual Product? Product { get; set; }
}
