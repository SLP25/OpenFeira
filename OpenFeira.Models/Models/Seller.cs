using System;
using System.Collections.Generic;

namespace OpenFeira.Data.Models;

public partial class Seller
{
    public string UserEmail { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public virtual ICollection<Sale> Sales { get; } = new List<Sale>();

    public virtual ICollection<Stand> Stands { get; } = new List<Stand>();

    public virtual User UserEmailNavigation { get; set; } = null!;
}
