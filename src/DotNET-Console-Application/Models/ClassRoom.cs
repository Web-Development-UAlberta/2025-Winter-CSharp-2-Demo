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

        // Implementation of abstract methods from Entity
        public override string GetDisplayString()
        {
            return RoomNumber;
        }

        public override void PopulateFromUserInput()
        {
            RoomNumber = Program.GetString("Please enter the Room Number: ");
        }

        public override void UpdateFromUserInput()
        {
            RoomNumber = Program.GetString("Please enter the new Room Number: ");
        }

        // Static helper method to create new classroom from user input
        public static ClassRoom CreateFromUserInput()
        {
            var classroom = new ClassRoom();
            classroom.PopulateFromUserInput();
            return classroom;
        }
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