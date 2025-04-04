using DotNET_Console_Application.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DotNET_Console_Application.Tests
{
    public class EntityTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<CodeFirstContext> _contextOptions;

        // Create a custom context for testing that overrides OnConfiguring
        private class TestCodeFirstContext : CodeFirstContext
        {
            private readonly DbContextOptions<CodeFirstContext> _options;

            public TestCodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
            {
                _options = options;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Don't call base.OnConfiguring to avoid trying to use environment variables
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlite("DataSource=:memory:");
                }
            }
        }

        public EntityTests()
        {
            // Create and open a SQLite in-memory database
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // Configure the context to use the in-memory SQLite database
            _contextOptions = new DbContextOptionsBuilder<CodeFirstContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema in the database
            using var context = new TestCodeFirstContext(_contextOptions);
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
            GC.SuppressFinalize(this);
        }

        // Test basic entity operations without using Entity.GetAll, etc.
        [Fact]
        public void Entity_CanBeCreatedAndSaved()
        {
            // Arrange
            using var context = new TestCodeFirstContext(_contextOptions);
            var classroom = new ClassRoom { RoomNumber = "101" };

            // Act
            context.ClassRooms.Add(classroom);
            context.SaveChanges();

            // Assert
            Assert.NotEqual(0, classroom.ID);

            var savedClassroom = context.ClassRooms.Find(classroom.ID);
            Assert.NotNull(savedClassroom);
            Assert.Equal("101", savedClassroom.RoomNumber);
        }

        [Fact]
        public void Entity_CanBeUpdated()
        {
            // Arrange
            using var context = new TestCodeFirstContext(_contextOptions);
            var classroom = new ClassRoom { RoomNumber = "101" };
            context.ClassRooms.Add(classroom);
            context.SaveChanges();
            int id = classroom.ID;

            // Act
            classroom.RoomNumber = "202";
            context.Update(classroom);
            context.SaveChanges();

            // Assert
            var updatedClassroom = context.ClassRooms.Find(id);
            Assert.Equal("202", updatedClassroom.RoomNumber);
        }

        [Fact]
        public void Entity_CanBeDeleted()
        {
            // Arrange
            using var context = new TestCodeFirstContext(_contextOptions);
            var classroom = new ClassRoom { RoomNumber = "101" };
            context.ClassRooms.Add(classroom);
            context.SaveChanges();
            int id = classroom.ID;

            // Act
            context.ClassRooms.Remove(classroom);
            context.SaveChanges();

            // Assert
            var deletedClassroom = context.ClassRooms.Find(id);
            Assert.Null(deletedClassroom);
        }

        [Fact]
        public void Entity_CanRetrieveAll()
        {
            // Arrange
            using var context = new TestCodeFirstContext(_contextOptions);
            context.ClassRooms.Add(new ClassRoom { RoomNumber = "101" });
            context.ClassRooms.Add(new ClassRoom { RoomNumber = "102" });
            context.SaveChanges();

            // Act
            var classrooms = context.ClassRooms.ToList();

            // Assert
            Assert.Equal(2, classrooms.Count);
            Assert.Contains(classrooms, r => r.RoomNumber == "101");
            Assert.Contains(classrooms, r => r.RoomNumber == "102");
        }

        [Fact]
        public void Entity_CanFindById()
        {
            // Arrange
            using var context = new TestCodeFirstContext(_contextOptions);
            var classroom = new ClassRoom { RoomNumber = "101" };
            context.ClassRooms.Add(classroom);
            context.SaveChanges();
            int id = classroom.ID;

            // Act
            var foundClassroom = context.ClassRooms.Find(id);

            // Assert
            Assert.NotNull(foundClassroom);
            Assert.Equal("101", foundClassroom.RoomNumber);
        }
    }
}