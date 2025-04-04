using System;
using System.IO;
using DotNET_Console_Application.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace DotNET_Console_Application.Tests
{
    /// <summary>
    /// Provides testing utilities for the application
    /// </summary>
    public static class TestHelpers
    {
        /// <summary>
        /// Creates a DbContextOptions instance for an in-memory SQLite database
        /// </summary>
        public static DbContextOptions<CodeFirstContext> CreateInMemoryDatabaseOptions()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return new DbContextOptionsBuilder<CodeFirstContext>()
                .UseSqlite(connection)
                .Options;
        }

        /// <summary>
        /// A test-friendly implementation of CodeFirstContext that doesn't rely on environment variables
        /// </summary>
        public class TestCodeFirstContext : CodeFirstContext
        {
            private readonly DbContextOptions<CodeFirstContext> _options;

            public TestCodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options)
            {
                _options = options;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                // Don't call base.OnConfiguring to avoid environment variable issues
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlite("DataSource=:memory:");
                }
            }
        }

        /// <summary>
        /// Captures console input/output for testing
        /// </summary>
        public class ConsoleCapture : IDisposable
        {
            private readonly TextReader _originalIn;
            private readonly TextWriter _originalOut;
            public StringReader InputReader { get; private set; }
            public StringWriter OutputWriter { get; private set; }

            public ConsoleCapture(string inputText)
            {
                _originalIn = Console.In;
                _originalOut = Console.Out;

                InputReader = new StringReader(inputText);
                OutputWriter = new StringWriter();

                Console.SetIn(InputReader);
                Console.SetOut(OutputWriter);
            }

            public void Dispose()
            {
                Console.SetIn(_originalIn);
                Console.SetOut(_originalOut);
                InputReader.Dispose();
                OutputWriter.Dispose();
            }
        }

        /// <summary>
        /// A test implementation of user input functions that don't rely on Console.ReadLine
        /// </summary>
        public static class TestInput
        {
            public static string GetString(TextReader reader, TextWriter writer, string prompt)
            {
                writer.Write(prompt);
                return reader.ReadLine()?.Trim() ?? string.Empty;
            }

            public static int GetInt(TextReader reader, TextWriter writer, string prompt)
            {
                writer.Write(prompt);
                string input = reader.ReadLine()?.Trim() ?? "0";
                if (int.TryParse(input, out int result))
                {
                    return result;
                }
                return 0;
            }

            public static int DisplayMenu(TextReader reader, TextWriter writer, string title, string[] options)
            {
                writer.WriteLine(title);
                for (int i = 0; i < options.Length; i++)
                {
                    writer.WriteLine($"{i + 1}. {options[i]}");
                }
                writer.Write("\tChoice: ");

                string input = reader.ReadLine()?.Trim() ?? "";
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Length)
                {
                    return choice;
                }

                writer.WriteLine("Sorry, invalid selection. Try again.");
                return -1;
            }
        }
    }
}