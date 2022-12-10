using System;
using System.Collections.Generic;

namespace OpenFeira.Data.Models;

public partial class Buyer
{
    public string UserEmail { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Bid> Bids { get; } = new List<Bid>();

    public virtual User UserEmailNavigation { get; set; } = null!;
}
