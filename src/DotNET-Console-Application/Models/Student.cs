using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Models
{
    [Table("student")]
    public partial class Student : Entity
    {
        [Column("course_id", TypeName = "INTEGER")]
        public int? ClassID { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [ForeignKey(nameof(ClassID))]
        [InverseProperty(nameof(Models.ClassRoom.Students))]
        public virtual ClassRoom ClassRoom { get; set; }

        // Implementation of abstract methods from Entity
        public override string GetDisplayString()
        {
            return $"{FirstName} {LastName}";
        }

        public override void PopulateFromUserInput()
        {
            FirstName = Program.GetString("Please enter the First Name: ");
            LastName = Program.GetString("Please enter the Last Name: ");
            ClassID = Program.GetInt("Please enter the Class ID: ");
        }

        public override void UpdateFromUserInput()
        {
            FirstName = Program.GetString("Please enter the new First Name: ");
            LastName = Program.GetString("Please enter the new Last Name: ");
        }

        // Static helper method to create new student from user input
        public static Student CreateFromUserInput()
        {
            var student = new Student();
            student.PopulateFromUserInput();
            return student;
        }
    }

    public partial class CodeFirstContext
    {
        public DbSet<Student> Students { get; set; }
        partial void OnModelCreatingPartialStudent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasData([new Student() {
                    ID = -1,
                    FirstName = "Jane",
                    LastName = "Doe",
                    ClassID = -1
                }]);
                entity.HasOne(child => child.ClassRoom)
                      .WithMany(parent => parent.Students)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName($"FK_{nameof(Student)}_{nameof(ClassRoom)}");

                entity.HasIndex(e => e.ClassID).HasDatabaseName($"FK_{nameof(Student)}_{nameof(ClassRoom)}");
            });
        }
    }
}