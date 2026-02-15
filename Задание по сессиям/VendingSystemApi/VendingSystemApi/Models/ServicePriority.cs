using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class ServicePriority
{
    public int ServicePriorityId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
