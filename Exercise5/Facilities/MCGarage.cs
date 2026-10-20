using System;
using System.Collections.Generic;
using System.Text;
using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    internal class MCGarage(string name, int capacity) : Garage(name, capacity)
    {
        public override VehicleType[] VehicleTypes => [VehicleType.Motorcycle];

        public override string PrintAvailableSlots()
        {
            return $"there are {base.AvailableSlots} slots available in the Garage";
        }
    }
}
