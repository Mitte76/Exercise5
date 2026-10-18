using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Facilities
{
    internal class Marina(string name, int parkingSpots) : Facility(name, parkingSpots)
    {
        public override void PrintAvailableSlots()
        {
            Console.WriteLine($"there are {base.AvailableSlots} slots available in the marina");
            Console.ReadKey();
        }
    }
}
