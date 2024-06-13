using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class PositionProduct
{
    public int IdPosProd { get; set; }

    public int? IdProd { get; set; }

    public int? IdPos { get; set; }

    public virtual Position? IdPosNavigation { get; set; }

    public virtual ProductPrice? IdProdNavigation { get; set; }
}
