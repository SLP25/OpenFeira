using System;
using System.Collections.Generic;

namespace OpenFeira.Data.Models;

public partial class Market
{
    public int MarketId { get; set; }

    public string MarketName { get; set; } = null!;

    public string MarketDescription { get; set; } = null!;

    public string? MarketLocation { get; set; }

    public int TotalStands { get; set; }

    public DateTime StartingTime { get; set; }

    public DateTime EndingTime { get; set; }

    public string OrganizerId { get; set; } = null!;

    public string MarketPhotoPath { get; set; } = null!;

    public virtual Organizer Organizer { get; set; } = null!;

    public virtual ICollection<Stand> Stands { get; } = new List<Stand>();
}
