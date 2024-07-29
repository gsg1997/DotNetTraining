using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_Copy
{
    class Person
    {
        public string Name;
        public Person(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return "Hello";
        }
        ~Person() { 
        Name = string.Empty;
        }

       

    }
    internal class Program
    {
        static void Main2(string[] args)
        {
            Person[] peopleNames = new Person[3];
            
            for (int i = 0; i<= peopleNames.Length-1; i++)
            {
                Console.WriteLine("Enter the Name");
                string NameofPeople = Console.ReadLine();
                peopleNames[i] = new Person(NameofPeople);
            }

            for (int i = 0;i<= peopleNames.Length-1;i++)
            {
                string fullNames = peopleNames[i].Name;
                Console.WriteLine("Hello My Name is " +fullNames);
            }
        }
    }
}
