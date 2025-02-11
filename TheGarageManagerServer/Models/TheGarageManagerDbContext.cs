using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TheGarageManagerServer.Models;

public partial class TheGarageManagerDbContext : DbContext
{
    public TheGarageManagerDbContext()
    {
    }

    public TheGarageManagerDbContext(DbContextOptions<TheGarageManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentStatus> AppointmentStatuses { get; set; }

    public virtual DbSet<CarRepair> CarRepairs { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<GaragePart> GarageParts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=TheGarageManagerDB;User ID=TheGarageManagerAdminLogin;Password=admin123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA278195EA6");

            entity.Property(e => e.AppointmentStatusId).HasDefaultValue(0);

            entity.HasOne(d => d.AppointmentStatus).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Appoi__398D8EEE");

            entity.HasOne(d => d.Garage).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Garag__37A5467C");

            entity.HasOne(d => d.LicensePlateNavigation).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Licen__38996AB5");
        });

        modelBuilder.Entity<AppointmentStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__Appointm__C8EE20631CB98744");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CarRepair>(entity =>
        {
            entity.HasKey(e => e.RepairId).HasName("PK__CarRepai__07D0BDCD880DAE07");

            entity.HasOne(d => d.Garage).WithMany(p => p.CarRepairs).HasConstraintName("FK__CarRepair__Garag__29572725");

            entity.HasOne(d => d.LicensePlateNavigation).WithMany(p => p.CarRepairs).HasConstraintName("FK__CarRepair__Licen__286302EC");
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("PK__Garage__5D8BEEB1F75995EB");

            entity.Property(e => e.GarageId).ValueGeneratedNever();
        });

        modelBuilder.Entity<GaragePart>(entity =>
        {
            entity.HasKey(e => e.PartId).HasName("PK__GaragePa__7C3F0D30F4F9162B");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageParts).HasConstraintName("FK__GaragePar__Garag__2C3393D0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACD85C3ED2");

            entity.HasOne(d => d.UserGarage).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__UserGarag__30F848ED");

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Users).HasConstraintName("FK__Users__UserStatu__31EC6D26");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__UserStat__C8EE204365815F62");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.LicensePlate).HasName("PK__Vehicle__026BC15DF3ED00E9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
