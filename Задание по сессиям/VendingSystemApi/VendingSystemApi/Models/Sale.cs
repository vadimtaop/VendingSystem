using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class Sale
{
    public int SaleId { get; set; }

    public Guid VendingMachineId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime Timestamp { get; set; }

    public decimal TotalPrice { get; set; }

    public int Quantity { get; set; }

    public int PaymentMethodId { get; set; }

    public virtual PaymentMethod PaymentMethod { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
