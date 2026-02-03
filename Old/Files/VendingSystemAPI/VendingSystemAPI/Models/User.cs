using System;
using System.Collections.Generic;

namespace VendingSystemAPI.Models;

public partial class User
{
    public int Id { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int Role { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
