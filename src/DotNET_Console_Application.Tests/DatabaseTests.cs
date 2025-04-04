using DotNET_Console_Application.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DotNET_Console_Application.Tests
{
    public class DatabaseTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<CodeFirstContext> _contextOptions;

        public DatabaseTests()
        {
            // Create and open a SQLite in-memory database
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // Configure the context to use the in-memory SQLite database
            _contextOptions = new DbContextOptionsBuilder<CodeFirstContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema in the database
            using var context = new CodeFirstContext(_contextOptions);
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
            GC.SuppressFinalize(this);
        }

        // Custom context for testing
        private class TestCodeFirstContext : CodeFirstContext
        {
            public TestCodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Ensure foreign key behavior is explicitly set for tests
                modelBuilder.Entity<Student>()
                    .HasOne(s => s.ClassRoom)
                    .WithMany(c => c.Students)
                    .HasForeignKey(s => s.ClassID)
                    .OnDelete(DeleteBehavior.SetNull);
            }
        }

        [Fact]
        public void CreateAndRetrieveClassRoom_WorksCorrectly()
        {
            // Arrange
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var classroom = new ClassRoom { RoomNumber = "Test101" };
                context.ClassRooms.Add(classroom);
                context.SaveChanges();
            }

            // Act
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var classroom = context.ClassRooms.FirstOrDefault(c => c.RoomNumber == "Test101");

                // Assert
                Assert.NotNull(classroom);
                Assert.Equal("Test101", classroom.RoomNumber);
            }
        }

        [Fact]
        public void CreateAndRetrieveStudent_WorksCorrectly()
        {
            // Arrange
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var student = new Student
                {
                    FirstName = "Test",
                    LastName = "Student",
                    ClassID = null
                };
                context.Students.Add(student);
                context.SaveChanges();
            }

            // Act
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var student = context.Students.FirstOrDefault(s => s.FirstName == "Test" && s.LastName == "Student");

                // Assert
                Assert.NotNull(student);
                Assert.Equal("Test", student.FirstName);
                Assert.Equal("Student", student.LastName);
                Assert.Null(student.ClassID);
            }
        }

        [Fact]
        public void Student_ClassRoom_Relationship_IsCorrectlyConfigured()
        {
            // Arrange
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                // Create a classroom
                var classroom = new ClassRoom { RoomNumber = "101A" };
                context.ClassRooms.Add(classroom);
                context.SaveChanges();

                // Create a student in that classroom
                var student = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    ClassID = classroom.ID
                };
                context.Students.Add(student);
                context.SaveChanges();
            }

            // Act & Assert
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var student = context.Students
                    .Include(s => s.ClassRoom)
                    .FirstOrDefault(s => s.FirstName == "John");

                Assert.NotNull(student);
                Assert.NotNull(student.ClassRoom);
                Assert.Equal("101A", student.ClassRoom.RoomNumber);
            }
        }

        [Fact]
        public void ClassRoom_Students_Relationship_IsCorrectlyConfigured()
        {
            // Arrange
            int classroomId;

            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                // Create a classroom and save to get its ID
                var classroom = new ClassRoom { RoomNumber = "101A" };
                context.ClassRooms.Add(classroom);
                context.SaveChanges();
                classroomId = classroom.ID;

                // Create students in that classroom - ensuring they're properly added
                context.Students.Add(new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    ClassID = classroomId
                });

                context.Students.Add(new Student
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    ClassID = classroomId
                });

                context.SaveChanges();
            }

            // Act - use a new context to verify the relationship
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                // Verify each student is properly linked to the classroom
                var students = context.Students.Where(s => s.ClassID == classroomId).ToList();
                Assert.Equal(2, students.Count);

                // Now retrieve the classroom with students included
                var classroom = context.ClassRooms
                    .Include(c => c.Students)
                    .FirstOrDefault(c => c.ID == classroomId);

                // Assert
                Assert.NotNull(classroom);
                Assert.Equal(2, classroom.Students.Count());
                Assert.Contains(classroom.Students, s => s.FirstName == "John" && s.LastName == "Doe");
                Assert.Contains(classroom.Students, s => s.FirstName == "Jane" && s.LastName == "Smith");
            }
        }

        [Fact]
        public void DeleteClassRoom_SetsForeignKeyToNull()
        {
            // Arrange
            int classroomId;
            int studentId;

            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                // Create schema to ensure relationship is defined properly for this test
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Create a classroom
                var classroom = new ClassRoom { RoomNumber = "101A" };
                context.ClassRooms.Add(classroom);
                context.SaveChanges();
                classroomId = classroom.ID;

                // Create a student in that classroom
                var student = new Student
                {
                    FirstName = "John",
                    LastName = "Doe",
                    ClassID = classroomId
                };
                context.Students.Add(student);
                context.SaveChanges();
                studentId = student.ID;

                // Verify setup is correct
                var studentBefore = context.Students.Find(studentId);
                Assert.Equal(classroomId, studentBefore.ClassID);
            }

            // Act - Delete the classroom (in a separate context to ensure changes are persisted)
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var classroom = context.ClassRooms.Find(classroomId);
                Assert.NotNull(classroom); // Ensure classroom exists

                context.ClassRooms.Remove(classroom);
                context.SaveChanges();
            }

            // Assert - Student should still exist but with ClassID set to null
            using (var context = new TestCodeFirstContext(_contextOptions))
            {
                var student = context.Students.Find(studentId);
                Assert.NotNull(student);
                Assert.Null(student.ClassID); // Now expect null after classroom is deleted
            }
        }
    }
}