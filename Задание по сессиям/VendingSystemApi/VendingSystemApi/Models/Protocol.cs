using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class Protocol
{
    public int Protocolid { get; set; }

    public int? VendingMachineId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Note { get; set; }
}
