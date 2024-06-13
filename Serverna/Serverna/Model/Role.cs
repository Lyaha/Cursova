using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<AccessRight> AccessRights { get; set; } = new List<AccessRight>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
