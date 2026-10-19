using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Facilities
{
    internal class MCGarage(string name, int capacity) : Garage(name, capacity)
    {
        public override string PrintAvailableSlots()
        {
            return $"there are {base.AvailableSlots} slots available in the marina";
            //Console.WriteLine($"there are {base.AvailableSlots} slots available in the marina");
            //Console.ReadKey();
        }
    }
}
