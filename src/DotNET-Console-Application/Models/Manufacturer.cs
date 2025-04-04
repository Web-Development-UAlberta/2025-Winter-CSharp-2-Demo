using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Models;

[Table("manufacturer")]
public partial class Manufacturer
{
    [Key]
    [Column("id")]
    public int ID { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [InverseProperty("Manufacturer")]
    public virtual ICollection<Model> Models { get; set; } = new List<Model>();

}

public partial class CarsContext
{
    public DbSet<Manufacturer> Manufacturers { get; set; }
    partial void OnModelCreatingPartialManufacturer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasData(
                new Manufacturer() { ID = -1, Name = "Mitsubishi" },
                new Manufacturer() { ID = -2, Name = "Honda" },
                new Manufacturer() { ID = -3, Name = "Toyota" },
                new Manufacturer() { ID = -4, Name = "Nissan" }
            );
        });
    }
}