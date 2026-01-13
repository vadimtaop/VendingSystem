using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class PaymentType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
