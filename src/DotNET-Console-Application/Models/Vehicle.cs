using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Models;

[Table("vehicle")]
public partial class Vehicle
{
    [Key]
    [Column("vin")]
    public string VIN { get; set; } = null!;

    [Column("model_id")]
    public int ModelID { get; set; }


    [Column("odometer")]
    public int Odometer { get; set; }

    [ForeignKey("ModelID")]
    [InverseProperty("Vehicles")]
    public virtual Model Model { get; set; } = null!;

}
public partial class CodeFirstContext
{
    public DbSet<Vehicle> Vehicles { get; set; }
    partial void OnModelCreatingPartialVehicle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasData([
                new Vehicle() { VIN = "4A3AL54F3XE067712", ModelID = -1, Odometer = 100 },
                    new Vehicle() { VIN = "JA3AN74K8XY001384", ModelID = -2,  Odometer = 100 },
                    new Vehicle() { VIN = "JH4NA1153MT000743", ModelID = -3, Odometer = 100 },
                    new Vehicle() { VIN = "JHMAP21475S008443", ModelID = -4, Odometer = 100 },
                    new Vehicle() { VIN = "JT2JA82J8S0028274", ModelID = -5, Odometer = 100 },
                    new Vehicle() { VIN = "JTDFR320320052403", ModelID = -6,  Odometer = 100 },
                    new Vehicle() { VIN = "JN1RZ24A1LX002317", ModelID = -7,  Odometer = 100 },
                    new Vehicle() { VIN = "-----BCNR33004655", ModelID = -8, Odometer = 100 },
                ]);
            entity.HasOne(child => child.Model)
                  .WithMany(parent => parent.Vehicles)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName($"FK_{nameof(Vehicle)}_{nameof(Model)}");

            entity.HasIndex(e => e.ModelID).HasDatabaseName($"FK_{nameof(Vehicle)}_{nameof(Model)}");
        });
    }
}