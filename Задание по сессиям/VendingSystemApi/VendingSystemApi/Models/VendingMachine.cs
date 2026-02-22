using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class VendingMachine
{
    public int VendingMachineId { get; set; }

    public string? Name { get; set; }

    public string? Model { get; set; }

    public string? Company { get; set; }

    public string? Modem { get; set; }

    public string? Location { get; set; }

    public DateTime? InstallDate { get; set; }

    public int? IntervalService { get; set; }

    public DateOnly? NextServiceDate { get; set; }

    public string? StatusMachine { get; set; }

    public string? StatusService { get; set; }

    public string? NameUser { get; set; }

    public string? Priority { get; set; }

    public string? ServiceType { get; set; }

    public DateOnly? StartServiceDate { get; set; }

    public int? DeadlineDays { get; set; }

    public string? Note { get; set; }

    public string? CancelNote { get; set; }
}
