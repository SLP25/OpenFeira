﻿using System;
using System.Collections.Generic;

namespace OpenFeira.Data;

public partial class UserToken
{
    public string UserEmail { get; set; } = null!;

    public byte[] Token { get; set; } = null!;

    public DateTime ExpiryDate { get; set; }

    public virtual User UserEmailNavigation { get; set; } = null!;
}
