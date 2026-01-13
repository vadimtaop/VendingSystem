using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class VendingMachine
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int MachineTypeId { get; set; }

    public decimal TotalProfit { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string InventoryNumber { get; set; } = null!;

    public string Manufacturer { get; set; } = null!;

    public DateOnly ManufactureDate { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly LastCheckDate { get; set; }

    public int InterCheckMonth { get; set; }

    public int ResourceHour { get; set; }

    public DateOnly NextCheckDate { get; set; }

    public int ServiceTime { get; set; }

    public int StatusId { get; set; }

    public int CountryId { get; set; }

    public DateOnly InventoryDate { get; set; }

    public DateOnly CreateDate { get; set; }

    public int CompanyId { get; set; }

    public int ModemId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual MachineType MachineType { get; set; } = null!;

    public virtual Modem Modem { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();

    public virtual MachineStatus Status { get; set; } = null!;
}
