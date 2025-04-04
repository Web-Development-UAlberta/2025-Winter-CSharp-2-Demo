using DotNET_Console_Application.Models;

namespace DotNET_Console_Application;



class Program
{
    static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine().Trim();
    }
    static void Main(string[] args)
    {
        string[] entities = ["ClassRoom", "Student"];
        int entityChoice;
        do
        {
            Console.Write("School Program\n1. ClassRoom\n2. Student\n3. Exit\n\tChoice: ");
            if (int.TryParse(Console.ReadLine().Trim(), out entityChoice))
            {
                entityChoice--;
                if (entityChoice >= 0 && entityChoice <= 1)
                {
                    int operationChoice;
                    do
                    {

                        Console.Write($"School Program - {entities[entityChoice]}\n1. Create\n2. Read\n3. Update\n4. Delete\n5. Exit\n\tChoice: ");
                        if (int.TryParse(Console.ReadLine().Trim(), out operationChoice))
                        {
                            if (operationChoice == 1)
                            {
                                if (entityChoice == 0)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        context.ClassRooms.Add(new ClassRoom()
                                        {
                                            RoomNumber = GetString("Please enter the Room Number: "),
                                        });
                                        context.SaveChanges();
                                    }
                                }
                                else if (entityChoice == 1)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        context.Students.Add(new Student()
                                        {
                                            FirstName = GetString("Please enter the First Name: "),
                                            LastName = GetString("Please enter the Last Name: "),
                                            ClassID = int.Parse(GetString("Please enter the Class ID: "))
                                        });
                                        context.SaveChanges();
                                    }
                                }

                            }
                            else if (operationChoice == 2)
                            {
                                if (entityChoice == 0)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        foreach (ClassRoom classRoom in context.ClassRooms.ToList())
                                        {
                                            Console.WriteLine($"{classRoom.ID}. {classRoom.RoomNumber}");
                                        }
                                    }
                                }
                                else if (entityChoice == 1)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        foreach (Student student in context.Students.ToList())
                                        {
                                            Console.WriteLine($"{student.ID}. {student.FirstName} {student.LastName}");
                                        }
                                    }
                                }
                            }
                            else if (operationChoice == 3)
                            {
                                if (entityChoice == 0)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        foreach (ClassRoom classRoom in context.ClassRooms.ToList())
                                        {
                                            Console.WriteLine($"{classRoom.ID}. {classRoom.RoomNumber}");
                                        }
                                        int targetID = int.Parse(GetString("Please enter the classroom ID to update: "));
                                        ClassRoom? target = context.ClassRooms.Find(targetID);
                                        if (target == null)
                                        {
                                            Console.WriteLine("Could not find that classroom, please try again.");
                                        }
                                        else
                                        {
                                            target.RoomNumber = GetString("Please enter the new Room Number: ");
                                            context.SaveChanges();
                                        }
                                    }
                                }
                                else if (entityChoice == 1)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        foreach (Student student in context.Students.ToList())
                                        {
                                            Console.WriteLine($"{student.ID}. {student.FirstName} {student.LastName}");
                                        }
                                        int targetID = int.Parse(GetString("Please enter the student ID to update: "));
                                        Student? target = context.Students.Find(targetID);
                                        if (target == null)
                                        {
                                            Console.WriteLine("Could not find that student, please try again.");
                                        }
                                        else
                                        {
                                            target.FirstName = GetString("Please enter the new First Name: ");
                                            target.LastName = GetString("Please enter the new Last Name: ");
                                            context.SaveChanges();
                                        }
                                    }
                                }
                            }
                            else if (operationChoice == 4)
                            {
                                if (entityChoice == 0)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        foreach (ClassRoom classRoom in context.ClassRooms.ToList())
                                        {
                                            Console.WriteLine($"{classRoom.ID}. {classRoom.RoomNumber}");
                                        }
                                        int targetID = int.Parse(GetString("Please enter the classroom ID to delete: "));
                                        ClassRoom? target = context.ClassRooms.Find(targetID);
                                        if (target == null)
                                        {
                                            Console.WriteLine("Could not find that classroom, please try again.");
                                        }
                                        else
                                        {
                                            context.Remove(target);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                                else if (entityChoice == 1)
                                {
                                    using (CodeFirstContext context = new CodeFirstContext())
                                    {
                                        foreach (Student student in context.Students.ToList())
                                        {
                                            Console.WriteLine($"{student.ID}. {student.FirstName} {student.LastName}");
                                        }
                                        int targetID = int.Parse(GetString("Please enter the student ID to update: "));
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
                            else if (operationChoice != 5)
                            {
                                Console.WriteLine("Sorry, invalid selection. Try again.");
                            }
                        }
                    } while (operationChoice != 5);
                }
                else if (entityChoice != 2)
                {
                    Console.WriteLine("Sorry, invalid selection. Try again.");

                }
            }
            else
            {
                Console.WriteLine("Sorry, invalid selection. Try again.");
            }

        } while (entityChoice != 2);
    }
}
