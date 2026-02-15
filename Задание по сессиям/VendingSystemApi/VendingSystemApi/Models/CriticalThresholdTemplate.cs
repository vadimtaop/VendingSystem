using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class CriticalThresholdTemplate
{
    public int CriticalThresholdTemplateId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachines { get; set; } = new List<VendingMachine>();
}
