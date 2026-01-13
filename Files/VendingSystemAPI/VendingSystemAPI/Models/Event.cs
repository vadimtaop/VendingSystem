using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class Event
{
    public int Id { get; set; }

    public int VendingMachineId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime EventDate { get; set; }

    public int Priority { get; set; }

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
