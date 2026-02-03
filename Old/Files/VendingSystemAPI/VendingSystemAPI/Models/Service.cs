using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class Service
{
    public int Id { get; set; }

    public int VendingMachineId { get; set; }

    public DateOnly ServiceDate { get; set; }

    public string? Description { get; set; }

    public string? Problems { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
