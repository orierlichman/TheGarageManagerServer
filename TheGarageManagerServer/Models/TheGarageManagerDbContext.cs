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

    public virtual DbSet<AppUser> AppUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=TheGarageManagerDB;User ID=TheGarageManagerAdminLogin;Password=thePassword;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppUsers__3214EC07B75679DE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
