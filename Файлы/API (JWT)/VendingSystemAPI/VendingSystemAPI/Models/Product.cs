using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int CountInStoke { get; set; }

    public int MinStoke { get; set; }

    public decimal AvgDailySales { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
