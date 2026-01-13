using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VendingSystemAPI.Models;

public partial class VendingSystemDbContext : DbContext
{
    public VendingSystemDbContext()
    {
    }

    public VendingSystemDbContext(DbContextOptions<VendingSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<MachineStatus> MachineStatuses { get; set; }

    public virtual DbSet<MachineType> MachineTypes { get; set; }

    public virtual DbSet<Modem> Modems { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=VendingSystemDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.EventDate).HasColumnType("datetime");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Events)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Events_VendingMachines");
        });

        modelBuilder.Entity<MachineStatus>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<MachineType>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Modem>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.AvgDailySales).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.TotalSum).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PaymentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_PaymentTypes");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_Products");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Sales)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sales_VendingMachines");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Problems).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.Services)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_Users");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Services)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Services_VendingMachines");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patronymic).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(255);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasIndex(e => e.InventoryNumber, "UQ_InventoryNumber").IsUnique();

            entity.HasIndex(e => e.SerialNumber, "UQ_SerialNumber").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.CreateDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.InventoryNumber).HasMaxLength(255);
            entity.Property(e => e.Manufacturer).HasMaxLength(255);
            entity.Property(e => e.Model).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.SerialNumber).HasMaxLength(255);
            entity.Property(e => e.TotalProfit).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Company).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Companies");

            entity.HasOne(d => d.Country).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Countries");

            entity.HasOne(d => d.MachineType).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.MachineTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_MachineTypes");

            entity.HasOne(d => d.Modem).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.ModemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_Modems");

            entity.HasOne(d => d.Status).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VendingMachines_MachineStatuses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
