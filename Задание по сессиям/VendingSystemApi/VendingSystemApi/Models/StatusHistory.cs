using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class StatusHistory
{
    public int StatusHistoryId { get; set; }

    public int? VendingMachineId { get; set; }

    public string? OldStatus { get; set; }

    public string? NewStatus { get; set; }

    public DateOnly? Date { get; set; }
}
