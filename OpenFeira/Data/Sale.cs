using System;
using System.Collections.Generic;

namespace OpenFeira.Data;

public partial class Sale
{
    public int SaleId { get; set; }

    public string SellerId { get; set; } = null!;

    public DateTime SaleTimestamp { get; set; }

    public int BidId { get; set; }

    public virtual Bid Bid { get; set; } = null!;

    public virtual Seller Seller { get; set; } = null!;
}
