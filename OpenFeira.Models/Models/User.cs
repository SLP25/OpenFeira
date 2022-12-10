using System;
using System.Collections.Generic;

namespace OpenFeira.Data.Models;

public partial class User
{
    public string UserEmail { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int Nif { get; set; }

    public string? Role { get; set; }

    public virtual Buyer? Buyer { get; set; }

    public virtual Organizer? Organizer { get; set; }

    public virtual Seller? Seller { get; set; }

    public virtual UserToken? UserToken { get; set; }
}
