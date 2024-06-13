using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int? BuyerId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? StatusId { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual OrderStatus? Status { get; set; }
}
