using System;
using System.Collections.Generic;

namespace OpenFeira.Data;

public partial class Stand
{
    public int StandId { get; set; }

    public string SellerId { get; set; } = null!;

    public int MarketId { get; set; }

    public string? StandPhotoPath { get; set; }

    public virtual ICollection<Bid> Bids { get; } = new List<Bid>();

    public virtual Market Market { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;

    public virtual ICollection<ProductInStand> ProductInStands { get; } = new List<ProductInStand>();
}
