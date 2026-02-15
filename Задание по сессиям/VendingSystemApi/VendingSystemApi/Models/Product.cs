using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int MinStoke { get; set; }

    public Guid VendingMachineId { get; set; }

    public string Description { get; set; } = null!;

    public int QuantityAvailable { get; set; }

    public decimal SalesTrend { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
