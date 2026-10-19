using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Facilities
{
    internal class CarGarage(string name, int capacity) : Garage(name, capacity)
    {
        public override string PrintAvailableSlots()
        {
            return $"There are {base.AvailableSlots} slots available in the garage. "
                + $"{OccupiedSlots} vehicle{(OccupiedSlots == 1 ? "" : "s")} currently parked.";
        }
    }
}
