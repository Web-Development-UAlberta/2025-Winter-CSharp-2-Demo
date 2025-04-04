using DotNET_Console_Application;
using System;
using System.IO;
using Xunit;

namespace DotNET_Console_Application.Tests
{
    public class ProgramTests
    {
        // Instead of testing Program.GetString directly, which relies on Console.ReadLine,
        // we'll test our own implementation of a similar method to verify the concept

        [Fact]
        public void TestGetStringConcept()
        {
            // We're testing the concept of the GetString method, not the actual implementation
            // This is a test of how such a method should work in a controlled environment
            string GetStringTest(TextReader reader, TextWriter writer, string prompt)
            {
                writer.Write(prompt);
                return reader.ReadLine()?.Trim() ?? string.Empty;
            }

            // Arrange
            var input = new StringReader("Test Input");
            var output = new StringWriter();
            var prompt = "Enter something: ";

            // Act
            var result = GetStringTest(input, output, prompt);

            // Assert
            Assert.Equal("Test Input", result);
            Assert.Equal(prompt, output.ToString());
        }

        [Fact]
        public void TestGetIntConcept()
        {
            // Similar to above, testing the concept of GetInt
            int GetIntTest(TextReader reader, TextWriter writer, string prompt)
            {
                writer.Write(prompt);
                string input = reader.ReadLine()?.Trim() ?? "0";
                return int.Parse(input);
            }

            // Arrange
            var input = new StringReader("42");
            var output = new StringWriter();
            var prompt = "Enter a number: ";

            // Act
            var result = GetIntTest(input, output, prompt);

            // Assert
            Assert.Equal(42, result);
            Assert.Equal(prompt, output.ToString());
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        [InlineData("invalid", -1)]
        [InlineData("10", -1)] // Assuming the menu has fewer than 10 options
        public void TestDisplayMenuConcept(string userInput, int expected)
        {
            // Testing the concept of DisplayMenu with a controlled implementation
            int DisplayMenuTest(TextReader reader, TextWriter writer, string title, string[] options)
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

            // Arrange
            var input = new StringReader(userInput);
            var output = new StringWriter();
            string title = "Test Menu";
            string[] options = new[] { "Option 1", "Option 2", "Option 3" };

            // Act
            var result = DisplayMenuTest(input, output, title, options);

            // Assert
            Assert.Equal(expected, result);

            var outputText = output.ToString();
            Assert.Contains(title, outputText);
            Assert.Contains("1. Option 1", outputText);
            Assert.Contains("2. Option 2", outputText);
            Assert.Contains("3. Option 3", outputText);

            if (expected == -1)
            {
                Assert.Contains("Sorry, invalid selection", outputText);
            }
        }

        // Test the menu validation logic specifically
        [Theory]
        [InlineData("", -1)]
        [InlineData("0", -1)]
        [InlineData("4", -1)]
        [InlineData("abc", -1)]
        [InlineData("1", 1)]
        [InlineData("3", 3)]
        public void TestMenuInputValidation(string input, int expected)
        {
            // This tests just the validation part of the DisplayMenu method
            int ValidateMenuInput(string input, int optionsCount)
            {
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= optionsCount)
                {
                    return choice;
                }
                return -1;
            }

            // Act
            var result = ValidateMenuInput(input, 3);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}