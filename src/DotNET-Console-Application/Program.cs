﻿namespace DotNET_Console_Application;

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person();
        Console.WriteLine($"{person.FirstName} {person.LastName}");
        person.FirstName = "Jane";
        Console.WriteLine($"{person.FirstName} {person.LastName}");
    }
}
