using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Models;

public partial class CarsContext : DbContext
{
    public CarsContext()
    {
    }

    public CarsContext(DbContextOptions<CarsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=assignment05.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartialModel(modelBuilder);
        OnModelCreatingPartialManufacturer(modelBuilder);
        OnModelCreatingPartialVehicle(modelBuilder);
    }

    partial void OnModelCreatingPartialModel(ModelBuilder modelBuilder);
    partial void OnModelCreatingPartialManufacturer(ModelBuilder modelBuilder);
    partial void OnModelCreatingPartialVehicle(ModelBuilder modelBuilder);
}
