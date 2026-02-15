using System;
using System.Collections.Generic;

namespace VendingSystemApi.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsManager { get; set; }

    public bool IsEngineer { get; set; }

    public bool IsOperator { get; set; }

    public string Phone { get; set; } = null!;

    public int RoleId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<VendingMachine> VendingMachineUserEngineers { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineUserIdManagerNavigations { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineUserIdTechnicianNavigations { get; set; } = new List<VendingMachine>();

    public virtual ICollection<VendingMachine> VendingMachineUsers { get; set; } = new List<VendingMachine>();
}
