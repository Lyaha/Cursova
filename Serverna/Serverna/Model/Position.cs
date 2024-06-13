using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class Position
{
    public int IdPosition { get; set; }

    public int? Row { get; set; }

    public int? Rack { get; set; }

    public int? Place { get; set; }

    public virtual ICollection<PositionProduct> PositionProducts { get; set; } = new List<PositionProduct>();
}
