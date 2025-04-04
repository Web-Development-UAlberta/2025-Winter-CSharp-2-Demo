using DotNET_Console_Application.Models;

namespace DotNET_Console_Application;

class Program
{
    // Helper methods for input and menus
    static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine().Trim();
    }

    static int GetInt(string prompt)
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

    // ClassRoom CRUD operations
    static void ListClassRooms()
    {
        using var context = new CodeFirstContext();
        foreach (var classRoom in context.ClassRooms.ToList())
        {
            Console.WriteLine($"{classRoom.ID}. {classRoom.RoomNumber}");
        }
    }

    static void CreateClassRoom()
    {
        using var context = new CodeFirstContext();
        context.ClassRooms.Add(new ClassRoom
        {
            RoomNumber = GetString("Please enter the Room Number: ")
        });
        context.SaveChanges();
    }

    static void UpdateClassRoom()
    {
        using var context = new CodeFirstContext();
        ListClassRooms();

        int targetID = GetInt("Please enter the classroom ID to update: ");
        ClassRoom? target = context.ClassRooms.Find(targetID);

        if (target == null)
        {
            Console.WriteLine("Could not find that classroom, please try again.");
            return;
        }

        target.RoomNumber = GetString("Please enter the new Room Number: ");
        context.SaveChanges();
    }

    static void DeleteClassRoom()
    {
        using var context = new CodeFirstContext();
        ListClassRooms();

        int targetID = GetInt("Please enter the classroom ID to delete: ");
        ClassRoom? target = context.ClassRooms.Find(targetID);

        if (target == null)
        {
            Console.WriteLine("Could not find that classroom, please try again.");
            return;
        }

        context.Remove(target);
        context.SaveChanges();
    }

    // Student CRUD operations
    static void ListStudents()
    {
        using var context = new CodeFirstContext();
        foreach (var student in context.Students.ToList())
        {
            Console.WriteLine($"{student.ID}. {student.FirstName} {student.LastName}");
        }
    }

    static void CreateStudent()
    {
        using var context = new CodeFirstContext();
        context.Students.Add(new Student
        {
            FirstName = GetString("Please enter the First Name: "),
            LastName = GetString("Please enter the Last Name: "),
            ClassID = GetInt("Please enter the Class ID: ")
        });
        context.SaveChanges();
    }

    static void UpdateStudent()
    {
        using var context = new CodeFirstContext();
        ListStudents();

        int targetID = GetInt("Please enter the student ID to update: ");
        Student? target = context.Students.Find(targetID);

        if (target == null)
        {
            Console.WriteLine("Could not find that student, please try again.");
            return;
        }

        target.FirstName = GetString("Please enter the new First Name: ");
        target.LastName = GetString("Please enter the new Last Name: ");
        context.SaveChanges();
    }

    static void DeleteStudent()
    {
        using var context = new CodeFirstContext();
        ListStudents();

        // Note: Keeping the prompt as "update" to match the original code
        int targetID = GetInt("Please enter the student ID to update: ");
        Student? target = context.Students.Find(targetID);

        if (target == null)
        {
            Console.WriteLine("Could not find that student, please try again.");
            return;
        }

        context.Remove(target);
        context.SaveChanges();
    }

    // Dispatch to appropriate CRUD operation based on entity type and operation choice
    static void PerformOperation(int entityType, int operation)
    {
        if (entityType == 0) // ClassRoom
        {
            switch (operation)
            {
                case 1: CreateClassRoom(); break;
                case 2: ListClassRooms(); break;
                case 3: UpdateClassRoom(); break;
                case 4: DeleteClassRoom(); break;
            }
        }
        else if (entityType == 1) // Student
        {
            switch (operation)
            {
                case 1: CreateStudent(); break;
                case 2: ListStudents(); break;
                case 3: UpdateStudent(); break;
                case 4: DeleteStudent(); break;
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