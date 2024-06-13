using System;
using System.Collections.Generic;

namespace Serverna.Model;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }
}
public partial class CreateUserModel
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public int RoleId { get; set;}
}

public partial class LoginUser
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public int? RoleId { get; set; }
}

public interface ICurrentUserService
{
    User CurrentUser { get; set; }
}

public class CurrentUserService : ICurrentUserService
{
    public User CurrentUser { get; set; }
}

