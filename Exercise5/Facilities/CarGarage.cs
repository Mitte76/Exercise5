using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Facilities
{
    internal class CarGarage(string name, int capacity) : Garage(name, capacity)
    {
        public override string PrintAvailableSlots()
        {
            return $"there are {base.AvailableSlots} slots available in the garage";
            //Console.WriteLine($"there are {base.AvailableSlots} slots available in the garage");
            //Console.ReadKey();
        }
    }
}
