using System;
using System.Collections.Generic;

namespace BlazorApp1.Data;

public partial class Bid
{
    public int BidId { get; set; }

    public int BidProduct { get; set; }

    public int BidStand { get; set; }

    public string BuyerId { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime BidTimestamp { get; set; }

    public int BidAmount { get; set; }



    public virtual Product BidProductNavigation { get; set; } = null!;

    public virtual Stand BidStandNavigation { get; set; } = null!;

    public virtual Buyer Buyer { get; set; } = null!;

    public virtual Sale? Sale { get; set; }
}
