using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class MachineType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
