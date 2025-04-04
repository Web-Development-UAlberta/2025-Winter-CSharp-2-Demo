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


