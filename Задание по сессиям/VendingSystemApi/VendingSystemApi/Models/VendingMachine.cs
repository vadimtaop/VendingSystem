using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class VendingMachine
{
    public Guid VendingMachineId { get; set; }

    public string Location { get; set; } = null!;

    public string Model { get; set; } = null!;

    public decimal TotalIncome { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string InventoryNumber { get; set; } = null!;

    public int CompanyId { get; set; }

    public DateTime ManufactureDate { get; set; }

    public DateTime InstallDate { get; set; }

    public DateTime LastMaintenanceDate { get; set; }

    public int? IntervalMonthCheck { get; set; }

    public int? ResourceHours { get; set; }

    public DateTime? NextMaintenanceDate { get; set; }

    public int? MaintenanceTimeHours { get; set; }

    public int StatusId { get; set; }

    public int CountryId { get; set; }

    public DateTime? InventoryDate { get; set; }

    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string RfidCashCollection { get; set; } = null!;

    public string? Notes { get; set; }

    public int WorkModeId { get; set; }

    public string RfidLoading { get; set; } = null!;

    public string KitOnlineNumber { get; set; } = null!;

    public int? CriticalThresholdTemplateId { get; set; }

    public int ServicePriorityId { get; set; }

    public Guid UserIdManager { get; set; }

    public int? NotificationTemplateId { get; set; }

    public string WorkingHours { get; set; } = null!;

    public Guid UserEngineerId { get; set; }

    public int PlaceId { get; set; }

    public int OperatorId { get; set; }

    public Guid UserIdTechnician { get; set; }

    public string RfidService { get; set; } = null!;

    public string CoordinateX { get; set; } = null!;

    public string CoordinateY { get; set; } = null!;

    public string Timezone { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual CriticalThresholdTemplate? CriticalThresholdTemplate { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual NotificationTemplate? NotificationTemplate { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual Place Place { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ServicePriority ServicePriority { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual User UserEngineer { get; set; } = null!;

    public virtual User UserIdManagerNavigation { get; set; } = null!;

    public virtual User UserIdTechnicianNavigation { get; set; } = null!;

    public virtual WorkMod WorkMode { get; set; } = null!;

    public virtual ICollection<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
}
