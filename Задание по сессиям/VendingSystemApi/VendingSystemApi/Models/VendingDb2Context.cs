using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VendingSystemApi.Models;

public partial class VendingDb2Context : DbContext
{
    public VendingDb2Context()
    {
    }

    public VendingDb2Context(DbContextOptions<VendingDb2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<MessageHistory> MessageHistories { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Protocol> Protocols { get; set; }

    public virtual DbSet<StatusHistory> StatusHistories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=VendingDb2;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MessageHistory>(entity =>
        {
            entity.HasKey(e => e.MessageHistotyId);

            entity.Property(e => e.Type).HasMaxLength(100);
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewId);
        });

        modelBuilder.Entity<Protocol>(entity =>
        {
            entity.Property(e => e.Note).HasMaxLength(100);
        });

        modelBuilder.Entity<StatusHistory>(entity =>
        {
            entity.Property(e => e.NewStatus).HasMaxLength(100);
            entity.Property(e => e.OldStatus).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Users_1");

            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(100);
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.InstallDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.Modem).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NameUser).HasMaxLength(100);
            entity.Property(e => e.Priority).HasMaxLength(100);
            entity.Property(e => e.ServiceType).HasMaxLength(100);
            entity.Property(e => e.StatusMachine).HasMaxLength(100);
            entity.Property(e => e.StatusService).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
