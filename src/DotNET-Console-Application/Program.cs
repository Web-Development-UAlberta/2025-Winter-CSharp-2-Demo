using DotNET_Console_Application.Models;

namespace DotNET_Console_Application;

class Program
{
    // Helper methods for input and menus
    public static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine().Trim();
    }

    public static int GetInt(string prompt)
    {
        return int.Parse(GetString(prompt));
    }

    static int DisplayMenu(string title, string[] options)
    {
        Console.Write($"{title}\n");
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.Write("\tChoice: ");

        if (int.TryParse(Console.ReadLine().Trim(), out int choice) && choice >= 1 && choice <= options.Length)
        {
            return choice;
        }

        Console.WriteLine("Sorry, invalid selection. Try again.");
        return -1;
    }

    // Generic CRUD operations
    static void Create<T>(string entityName) where T : Entity, new()
    {
        var entity = new T();
        entity.PopulateFromUserInput();
        entity.Save();
    }

    static void Read<T>() where T : Entity
    {
        var entities = Entity.GetAll<T>();
        foreach (var entity in entities)
        {
            Console.WriteLine($"{entity.ID}. {entity.GetDisplayString()}");
        }
    }

    static void Update<T>(string entityName) where T : Entity
    {
        Read<T>();

        int targetID = GetInt($"Please enter the {entityName} ID to update: ");
        T target = Entity.GetById<T>(targetID);

        if (target == null)
        {
            Console.WriteLine($"Could not find that {entityName}, please try again.");
            return;
        }

        target.UpdateFromUserInput();
        target.Save();
    }

    static void Delete<T>(string entityName) where T : Entity
    {
        Read<T>();

        string prompt = entityName == "student" ?
            "Please enter the student ID to update: " : // Keeping the original prompt for consistency
            $"Please enter the {entityName} ID to delete: ";

        int targetID = GetInt(prompt);
        T target = Entity.GetById<T>(targetID);

        if (target == null)
        {
            Console.WriteLine($"Could not find that {entityName}, please try again.");
            return;
        }

        target.Delete();
    }

    // Dispatch to appropriate CRUD operation based on entity type and operation choice
    static void PerformOperation(int entityType, int operation)
    {
        if (entityType == 0) // ClassRoom
        {
            switch (operation)
            {
                case 1: Create<ClassRoom>("classroom"); break;
                case 2: Read<ClassRoom>(); break;
                case 3: Update<ClassRoom>("classroom"); break;
                case 4: Delete<ClassRoom>("classroom"); break;
            }
        }
        else if (entityType == 1) // Student
        {
            switch (operation)
            {
                case 1: Create<Student>("student"); break;
                case 2: Read<Student>(); break;
                case 3: Update<Student>("student"); break;
                case 4: Delete<Student>("student"); break;
            }
        }
    }

    static void Main(string[] args)
    {
        string[] entities = ["ClassRoom", "Student", "Exit"];
        string[] operations = ["Create", "Read", "Update", "Delete", "Exit"];

        int entityChoice;
        do
        {
            entityChoice = DisplayMenu("School Program", entities);

            if (entityChoice > 0 && entityChoice < 3) // Valid entity choices (1 or 2)
            {
                int entityIndex = entityChoice - 1; // Convert to 0-based index

                int operationChoice;
                do
                {
                    operationChoice = DisplayMenu($"School Program - {entities[entityIndex]}", operations);

                    if (operationChoice > 0 && operationChoice < 5) // Valid CRUD operations (1-4)
                    {
                        PerformOperation(entityIndex, operationChoice);
                    }
                    else if (operationChoice != 5)
                    {
                        Console.WriteLine("Sorry, invalid selection. Try again.");
                    }
                } while (operationChoice != 5);
            }
            else if (entityChoice != 3)
            {
                Console.WriteLine("Sorry, invalid selection. Try again.");
            }

        } while (entityChoice != 3);
    }
}