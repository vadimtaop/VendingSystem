using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int VendingMachineId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public decimal TotalSum { get; set; }

    public DateTime SaleDate { get; set; }

    public int PaymentTypeId { get; set; }

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
