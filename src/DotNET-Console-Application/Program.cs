using DotNET_Console_Application.Models;
using static DotNET_Console_Application.Functions;

namespace DotNET_Console_Application;

class Program
{
  static void Main(string[] args)
  {
    string[] entities = ["Instructor", "Course", "Student"];
    int entityChoice;
    do
    {
      Console.Write("School Program\n1. Instructor\n2. Course\n3. Student\n4. Exit\n");
      entityChoice = GetInteger("\tChoice: ") - 1;
      if (entityChoice >= 0 && entityChoice <= 2)
      {
        int operationChoice;
        do
        {

          Console.Write($"School Program - {entities[entityChoice]}\n1. Create\n2. Read\n3. Update\n4. Delete\n5. Exit\n");
          operationChoice = GetInteger("\tChoice: ");
          if (operationChoice == 1)
          {
            if (entityChoice == 0)
            {
              AddInstructor();
            }
            else if (entityChoice == 1)
            {
              AddCourse();
            }
            else if (entityChoice == 2)
            {
              AddStudent();
            }

          }
          else if (operationChoice == 2)
          {
            if (entityChoice == 0)
            {
              DisplayInstructors();
            }
            else if (entityChoice == 1)
            {
              DisplayCourses();
            }
            else if (entityChoice == 2)
            {
              DisplayStudents();
            }
          }
          else if (operationChoice == 3)
          {
            if (entityChoice == 0)
            {
              UpdateInstructor();
            }
            else if (entityChoice == 1)
            {
              UpdateCourse();
            }
            else if (entityChoice == 2)
            {
              UpdateStudent();
            }
          }
          else if (operationChoice == 4)
          {
            if (entityChoice == 0)
            {
              DeleteInstructor();
            }
            else if (entityChoice == 1)
            {
              DeleteCourse();
            }
            else if (entityChoice == 2)
            {
              DeleteStudent();
            }
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
