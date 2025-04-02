using DotNET_Console_Application.Models;

namespace DotNET_Console_Application;



class Functions
{
  public static string GetString(string prompt)
  {
    while (true)
    {
      Console.Write(prompt);
      string input = Console.ReadLine()!.Trim();
      if (input != "") return input;
      else Console.WriteLine("Input cannot be empty!");
    }
  }

  public static int GetInteger(string prompt)
  {
    while (true)
    {
      Console.Write(prompt);
      string input = Console.ReadLine()!.Trim();
      if (input != "")
      {
        if (int.TryParse(input, out int num)) return num;
        else Console.WriteLine("Input must be an integer!");
      }
      else
      {
        Console.WriteLine("Input cannot be empty!");
      }
    }

  }

  public static void AddInstructor()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      context.Instructors.Add(new Instructor()
      {
        FirstName = GetString("Please enter the First Name: "),
        LastName = GetString("Please enter the Last Name: "),
      });
      context.SaveChanges();
    }
  }

  public static void AddCourse()
  {
    string code = GetString("Please enter the Course Code: ");
    string name = GetString("Please enter the Name: ");
    DisplayInstructors();
    int instructorID = GetInteger("Please enter the Instructor ID: ");
    using (CodeFirstContext context = new CodeFirstContext())
    {
      context.Courses.Add(new Course()
      {
        Code = code,
        Name = name,
        InstructorID = instructorID
      });
      context.SaveChanges();
    }
  }

  public static void AddStudent()
  {
    string firstName = GetString("Please enter the First Name: ");
    string lastName = GetString("Please enter the Last Name: ");
    DisplayCourses();
    int courseID = GetInteger("Please enter the Course ID: ");
    using (CodeFirstContext context = new CodeFirstContext())
    {
      context.Students.Add(new Student()
      {
        FirstName = firstName,
        LastName = lastName,
        CourseID = courseID
      });
      context.SaveChanges();
    }
  }

  public static void DisplayInstructors()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      foreach (Instructor instructor in context.Instructors.ToList())
      {
        Console.WriteLine($"{instructor.ID}. {instructor.FirstName} {instructor.LastName}");
      }
    }
  }

  public static void DisplayCourses()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      foreach (Course course in context.Courses.ToList())
      {
        Instructor target = context.Instructors.Find(course.InstructorID)!;
        Console.WriteLine($"{course.ID}. {course.Name} ({course.Code}) taught by {target.FirstName} {target.LastName}");
      }
    }
  }

  public static void DisplayStudents()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      foreach (Student student in context.Students.ToList())
      {
        Console.WriteLine($"{student.ID}. {student.FirstName} {student.LastName}");
      }
    }
  }

  public static void UpdateInstructor()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      DisplayInstructors();
      int targetID = GetInteger("Please enter the instructor ID to update: ");
      Instructor? target = context.Instructors.Find(targetID);
      if (target == null)
      {
        Console.WriteLine("Could not find that instructor, please try again.");
      }
      else
      {
        target.FirstName = GetString("Please enter the new First Name: ");
        target.LastName = GetString("Please enter the new Last Name: ");
        context.SaveChanges();
      }
    }
  }

  public static void UpdateCourse()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      DisplayCourses();
      int targetID = GetInteger("Please enter the course ID to update: ");
      Course? target = context.Courses.Find(targetID);
      if (target == null)
      {
        Console.WriteLine("Could not find that course, please try again.");
      }
      else
      {
        target.Name = GetString("Please enter the new course Name: ");
        target.Code = GetString("Please enter the new course Code: ");
        DisplayInstructors();
        target.InstructorID = GetInteger("Please enter the new instructor Id: ");
        context.SaveChanges();
      }
    }
  }

  public static void UpdateStudent()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      DisplayStudents();
      int targetID = GetInteger("Please enter the student ID to update: ");
      Student? target = context.Students.Find(targetID);
      if (target == null)
      {
        Console.WriteLine("Could not find that student, please try again.");
      }
      else
      {
        target.FirstName = GetString("Please enter the new First Name: ");
        target.LastName = GetString("Please enter the new Last Name: ");
        DisplayCourses();
        target.CourseID = GetInteger("Please enter the new course Id: ");
        context.SaveChanges();
      }
    }
  }

  public static void DeleteInstructor()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      DisplayInstructors();
      int targetID = GetInteger("Please enter the instructor ID to delete: ");
      Instructor? target = context.Instructors.Find(targetID);
      if (target == null)
      {
        Console.WriteLine("Could not find that instructor, please try again.");
      }
      else
      {
        context.Remove(target);
        context.SaveChanges();
      }
    }
  }

  public static void DeleteCourse()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      DisplayCourses();
      int targetID = GetInteger("Please enter the course ID to delete: ");
      Course? target = context.Courses.Find(targetID);
      if (target == null)
      {
        Console.WriteLine("Could not find that course, please try again.");
      }
      else
      {
        context.Remove(target);
        context.SaveChanges();
      }
    }
  }

  public static void DeleteStudent()
  {
    using (CodeFirstContext context = new CodeFirstContext())
    {
      DisplayStudents();
      int targetID = GetInteger("Please enter the student ID to delete: ");
      Student? target = context.Students.Find(targetID);
      if (target == null)
      {
        Console.WriteLine("Could not find that student, please try again.");
      }
      else
      {
        context.Remove(target);
        context.SaveChanges();
      }
    }
  }
}