using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VendingSystemApi.Models;

public partial class VendingDbContext : DbContext
{
    public VendingDbContext()
    {
    }

    public VendingDbContext(DbContextOptions<VendingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CriticalThresholdTemplate> CriticalThresholdTemplates { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<ServicePriority> ServicePriorities { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    public virtual DbSet<WorkMod> WorkMods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-R5THKSI\\SQLEXPRESS;Initial Catalog=VendingDb;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.CompanyId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.CountryId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<CriticalThresholdTemplate>(entity =>
        {
            entity.Property(e => e.CriticalThresholdTemplateId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.Property(e => e.MaintenanceId).ValueGeneratedNever();
            entity.Property(e => e.IssuesFound).HasMaxLength(200);
            entity.Property(e => e.MaintenanceDate).HasColumnType("datetime");
            entity.Property(e => e.WorkDescription).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maintenances_Users");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Maintenances_VendingMachines");
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.Property(e => e.NotificationTemplateId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.Property(e => e.OperatorId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.Property(e => e.PaymentMethodId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.Property(e => e.PaymentTypeId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.Property(e => e.PlaceId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SalesTrend).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_VendingMachines");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.SaleId).ValueGeneratedNever();
            entity.Property(e => e.Timestamp).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_PaymentMethods");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Products");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Sales)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_VendingMachines");
        });

        modelBuilder.Entity<ServicePriority>(entity =>
        {
            entity.Property(e => e.ServicePriorityId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasIndex(e => e.InventoryNumber, "UK_InventoryNumber").IsUnique();

            entity.HasIndex(e => e.SerialNumber, "UK_SerialNumber").IsUnique();

            entity.Property(e => e.VendingMachineId).ValueGeneratedNever();
            entity.Property(e => e.CoordinateX).HasMaxLength(50);
            entity.Property(e => e.CoordinateY).HasMaxLength(50);
            entity.Property(e => e.InstallDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryDate).HasColumnType("datetime");
            entity.Property(e => e.InventoryNumber).HasMaxLength(50);
            entity.Property(e => e.KitOnlineNumber).HasMaxLength(50);
            entity.Property(e => e.LastMaintenanceDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.ManufactureDate).HasColumnType("datetime");
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NextMaintenanceDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.RfidCashCollection).HasMaxLength(50);
            entity.Property(e => e.RfidLoading).HasMaxLength(50);
            entity.Property(e => e.RfidService).HasMaxLength(50);
            entity.Property(e => e.SerialNumber).HasMaxLength(50);
            entity.Property(e => e.Timezone).HasMaxLength(20);
            entity.Property(e => e.TotalIncome).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WorkingHours).HasMaxLength(50);

            entity.HasOne(d => d.Company).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Companies");

            entity.HasOne(d => d.Country).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Countries");

            entity.HasOne(d => d.CriticalThresholdTemplate).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CriticalThresholdTemplateId)
                .HasConstraintName("FK_VendingMachines_CriticalThresholdTemplates");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.NotificationTemplateId)
                .HasConstraintName("FK_VendingMachines_NotificationTemplates");

            entity.HasOne(d => d.Operator).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Operators");

            entity.HasOne(d => d.Place).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Places");

            entity.HasOne(d => d.ServicePriority).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.ServicePriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_ServicePriorities");

            entity.HasOne(d => d.Status).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Statuses");

            entity.HasOne(d => d.UserEngineer).WithMany(p => p.VendingMachineUserEngineers)
                .HasForeignKey(d => d.UserEngineerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Users1");

            entity.HasOne(d => d.User).WithMany(p => p.VendingMachineUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Users3");

            entity.HasOne(d => d.UserIdManagerNavigation).WithMany(p => p.VendingMachineUserIdManagerNavigations)
                .HasForeignKey(d => d.UserIdManager)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Users2");

            entity.HasOne(d => d.UserIdTechnicianNavigation).WithMany(p => p.VendingMachineUserIdTechnicianNavigations)
                .HasForeignKey(d => d.UserIdTechnician)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Users");

            entity.HasOne(d => d.WorkMode).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.WorkModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_WorkMods");

            entity.HasMany(d => d.PaymentTypes).WithMany(p => p.VendingMachines)
                .UsingEntity<Dictionary<string, object>>(
                    "VendingMachinePaymentType",
                    r => r.HasOne<PaymentType>().WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_VendingMachinePaymentTypes_PaymentTypes"),
                    l => l.HasOne<VendingMachine>().WithMany()
                        .HasForeignKey("VendingMachineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_VendingMachinePaymentTypes_VendingMachines"),
                    j =>
                    {
                        j.HasKey("VendingMachineId", "PaymentTypeId");
                        j.ToTable("VendingMachinePaymentTypes");
                    });
        });

        modelBuilder.Entity<WorkMod>(entity =>
        {
            entity.HasKey(e => e.WorkMode);

            entity.Property(e => e.WorkMode).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
