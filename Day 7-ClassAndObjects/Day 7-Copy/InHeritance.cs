using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_Copy
{
    class Persons
    {
        public void greet()
        {
            Console.WriteLine("Hello,");
        }

        public void SetAge(int age)
        {

            Console.WriteLine("  the age is " + age);
        }
    }
    class Student : Persons 
    {
        public void Study()
        {
            Console.WriteLine("I'm Studying on the Screen");
        }

        public void ShowAge()
        {
            Console.WriteLine("My age is :");
        }
    }

    class Teacher : Persons 
    {
        public void Explainy()
        {
            Console.WriteLine("I'm Teaching on the Screen");
        }

    }

    internal class StudentProffessor
    {
        static void Main(string[] args)
        {
            Persons newpersons = new Persons();
            newpersons.greet();
            Persons newstudent = new Persons();
            newstudent.greet();
            newstudent.SetAge(16);
            Teacher newTeachers = new Teacher();
            newTeachers.SetAge(35);
            newTeachers.greet();
            newTeachers.Explainy();



        }
    }
}
