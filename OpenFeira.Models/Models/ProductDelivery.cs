using System;
using System.Collections.Generic;

namespace OpenFeira.Data.Models;

public partial class ProductDelivery
{
    public int DeliveryId { get; set; }

    public int ProductId { get; set; }

    public int DeliveryAmount { get; set; }

    public DateTime DeliveryTimestamp { get; set; }

    public virtual Product Product { get; set; } = null!;
}
