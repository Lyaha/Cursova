using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class MovementType
{
    public int MovementTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
