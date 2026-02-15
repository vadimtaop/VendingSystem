using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class Maintenance
{
    public int MaintenanceId { get; set; }

    public Guid VendingMachineId { get; set; }

    public DateTime MaintenanceDate { get; set; }

    public string? WorkDescription { get; set; }

    public string? IssuesFound { get; set; }

    public Guid UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual VendingMachine VendingMachine { get; set; } = null!;
}
