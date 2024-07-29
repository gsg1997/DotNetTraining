using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIRSTAPPLICATION
{
    internal class Electricity
    {
        static void Main(string[] args)
        {
            int TotalUnits,TotalUnit2;
            Console.WriteLine("Enter Number 1");
            TotalUnits = Convert.ToInt32(Console.ReadLine());
           

            Console.WriteLine("Total Units Consumed is :"+ TotalUnits);
            
            if (TotalUnits <= 50)
            {
                Console.WriteLine("Total Amount is :"+TotalUnits*0.50);
            }
            if (TotalUnits >50 && TotalUnits <=100) 
                {

                TotalUnit2 = TotalUnits - 50;
                Console.WriteLine(TotalUnit2*0.75+ 50*0.50);
            }
            if (TotalUnits > 150 && TotalUnits <= 250)
            {
                TotalUnit2 = TotalUnits - 150;
                Console.WriteLine(TotalUnit2 * 1.20 + 100 * 0.75+50*0.50);
            }
            if (TotalUnits > 250)
            {
                TotalUnit2 = TotalUnits - 250;
                Console.WriteLine(TotalUnit2 * 1.50 + 100 * 1.20 + 100 * 0.75 + 50 * 0.50);
            }
        }
    }
}
