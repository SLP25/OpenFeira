using System;
using System.Collections.Generic;

namespace OpenFeira.Data;

public partial class ProductPhoto
{
    public int ProductId { get; set; }

    public string ProductPhotoPath { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
