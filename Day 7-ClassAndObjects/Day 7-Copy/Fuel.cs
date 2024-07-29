using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_7_Copy
{

   interface IVehiculo
    {
        void Drive();

        void Refuel(int gasolineAmount);
        
    } 

    class Vehicle : IVehiculo
    {
        void Drive()
        {
            
                Console.WriteLine("Car is Driving");

            
        }
        bool Refuel(int gasolineAmount)
        {
               if (gasolineAmount > 0)
            {
                return true;
            }
        }
    }
       
    internal class Fuel
    {

    }
}
