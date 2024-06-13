using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class AccessRight
{
    public int AccessId { get; set; }

    public int RoleId { get; set; }

    public bool CanCreateProduct { get; set; }

    public bool CanEditProduct { get; set; }

    public bool CanDeleteProduct { get; set; }

    public bool CanCreateStockMovement { get; set; }

    public bool CanEditStockMovement { get; set; }

    public bool CanCreateOrder { get; set; }

    public bool CanEditOrder { get; set; }

    public bool CanCreatePayment { get; set; }

    public bool CanEditPayment { get; set; }

    public bool CanManageUsers { get; set; }

    public bool CanManageRoles { get; set; }

    public bool CanManageAccessRights { get; set; }

    public virtual Role Role { get; set; } = null!;
}
