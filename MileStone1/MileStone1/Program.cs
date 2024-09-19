using System;
using System.Collections.Generic;

class Doctor
{
    public string RegistrationNo { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Specialization { get; set; }
    public string Address { get; set; }
    public string Timings { get; set; }
    public string ContactNo { get; set; }

    public override string ToString()
    {
        return $"Registration No: {RegistrationNo}\n" +
               $"Doctor Name: {Name}\n" +
               $"City: {City}\n" +
               $"Specialization: {Specialization}\n" +
               $"Clinic Address: {Address}\n" +
               $"Clinic Timings: {Timings}\n" +
               $"Contact No: {ContactNo}\n";
    }
}

class DoctorManagementSystem
{
    private List<Doctor> doctors = new List<Doctor>();

    public void AddDoctor()
    {
        Console.WriteLine("Enter Doctor Information:");

        var doctor = new Doctor
        {
            RegistrationNo = Prompt("Registration No: "),
            Name = Prompt("Doctor Name: "),
            City = Prompt("City: "),
            Specialization = Prompt("Area of Specialization: "),
            Address = Prompt("Clinic Address: "),
            Timings = Prompt("Clinic Timings: "),
            ContactNo = Prompt("Contact No: ")
        };

        doctors.Add(doctor);
        Console.WriteLine("Doctor information added successfully!\n");
    }

    public void DisplayDoctors()
    {
        if (doctors.Count == 0)
        {
            Console.WriteLine("No doctor information available.\n");
            return;
        }

        Console.WriteLine("List of Doctors:");
        foreach (var doctor in doctors)
        {
            Console.WriteLine(doctor);
        }
    }

    public void FindDoctor()
    {
        string specialization = Prompt("Enter area of specialization to search: ");
        var foundDoctors = doctors.FindAll(d => d.Specialization.IndexOf(specialization, StringComparison.OrdinalIgnoreCase) >= 0);

        if (foundDoctors.Count > 0)
        {
            Console.WriteLine($"Doctors specializing in {specialization}:");
            foreach (var doctor in foundDoctors)
            {
                Console.WriteLine(doctor);
            }
        }
        else
        {
            Console.WriteLine($"No doctors found specializing in {specialization}.\n");
        }
    }

    private string Prompt(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Doctor Management System");
            Console.WriteLine("1. Add Doctor");
            Console.WriteLine("2. Display Doctors");
            Console.WriteLine("3. Find Doctor by Specialization");
            Console.WriteLine("4. Exit");

            string choice = Prompt("Choose an option: ");
            switch (choice)
            {
                case "1":
                    AddDoctor();
                    break;
                case "2":
                    DisplayDoctors();
                    break;
                case "3":
                    FindDoctor();
                    break;
                case "4":
                    Console.WriteLine("Exiting the system. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var system = new DoctorManagementSystem();
        system.Run();
    }
}