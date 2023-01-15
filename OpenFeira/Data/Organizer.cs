using System;
using System.Collections.Generic;

namespace OpenFeira.Data;

public partial class Organizer
{
    public string UserEmail { get; set; } = null!;

    public string OrganizerName { get; set; } = null!;

    public virtual ICollection<Market> Markets { get; } = new List<Market>();

    public virtual User UserEmailNavigation { get; set; } = null!;
}
