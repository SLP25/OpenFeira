﻿using System;
using System.Collections.Generic;

namespace OpenFeira.Data.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public decimal ProductBasePrice { get; set; }

    public int ProductStock { get; set; }

    public virtual ICollection<Bid> Bids { get; } = new List<Bid>();

    public virtual ICollection<ProductDelivery> ProductDeliveries { get; } = new List<ProductDelivery>();

    public virtual ICollection<ProductPhoto> ProductPhotos { get; } = new List<ProductPhoto>();

    public virtual ICollection<Stand> Stands { get; } = new List<Stand>();
}
