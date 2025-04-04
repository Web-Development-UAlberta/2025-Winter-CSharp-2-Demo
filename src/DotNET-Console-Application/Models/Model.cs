using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Models;

[Table("model")]
public partial class Model
{
    [Key]
    [Column("id")]
    public int ID { get; set; }

    [Column("manufacturer_id")]
    public int ManufacturerID { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [ForeignKey("ManufacturerID")]
    [InverseProperty("Models")]
    public virtual Manufacturer Manufacturer { get; set; } = null!;

    [InverseProperty("Model")]
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

}
public partial class CodeFirstContext
{
    public DbSet<Model> Models { get; set; }
    partial void OnModelCreatingPartialModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasOne(child => child.Manufacturer)
                  .WithMany(parent => parent.Models)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName($"FK_{nameof(Model)}_{nameof(Manufacturer)}");

            entity.HasIndex(e => e.ManufacturerID).HasDatabaseName($"FK_{nameof(Model)}_{nameof(Manufacturer)}");

            // Seed data for Model
            entity.HasData(
                new Model() { ID = -1, ManufacturerID = -1, Name = "Eclipse" },
                new Model() { ID = -2, ManufacturerID = -1, Name = "3000GT" },
                new Model() { ID = -3, ManufacturerID = -2, Name = "NSX" },
                new Model() { ID = -4, ManufacturerID = -2, Name = "S2000" },
                new Model() { ID = -5, ManufacturerID = -3, Name = "Supra" },
                new Model() { ID = -6, ManufacturerID = -3, Name = "MR2" },
                new Model() { ID = -7, ManufacturerID = -4, Name = "300ZX" },
                new Model() { ID = -8, ManufacturerID = -4, Name = "GTR" }
            );
        });
    }
}