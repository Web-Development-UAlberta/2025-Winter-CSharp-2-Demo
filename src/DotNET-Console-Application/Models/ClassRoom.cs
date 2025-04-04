using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Models
{
    [Table("course")]
    public partial class ClassRoom : Entity
    {

        [Required]
        [Column("room_number", TypeName = "TEXT")]
        public string RoomNumber { get; set; }

        [InverseProperty(nameof(Models.Student.ClassRoom))]
        public virtual IEnumerable<Student> Students { get; set; }
    }
    public partial class CodeFirstContext
    {
        public DbSet<ClassRoom> ClassRooms { get; set; }
        partial void OnModelCreatingPartialCourse(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassRoom>(entity =>
            {

                // Seed data for Course
                entity.HasData(
                    new ClassRoom() { ID = -1, RoomNumber = "101A" },
                    new ClassRoom() { ID = -2, RoomNumber = "101B" }
                );
            });
        }
    }
}


