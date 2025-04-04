using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNET_Console_Application.Models
{
    public abstract class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id", TypeName = "INTEGER")]
        public int ID { get; set; }

        // CRUD Operations

        // Create
        public void Save()
        {
            using var context = new CodeFirstContext();
            if (ID == 0)
            {
                context.Add(this);
            }
            else
            {
                context.Update(this);
            }
            context.SaveChanges();
        }

        // Delete
        public void Delete()
        {
            using var context = new CodeFirstContext();
            context.Remove(this);
            context.SaveChanges();
        }

        // Static methods for generic entity operations

        // Read All
        public static List<T> GetAll<T>() where T : Entity
        {
            using var context = new CodeFirstContext();
            return context.Set<T>().ToList();
        }

        // Read By ID
        public static T GetById<T>(int id) where T : Entity
        {
            using var context = new CodeFirstContext();
            return context.Set<T>().Find(id);
        }

        // Abstract methods each entity must implement
        public abstract string GetDisplayString();
        public abstract void PopulateFromUserInput();
        public abstract void UpdateFromUserInput();
    }
}