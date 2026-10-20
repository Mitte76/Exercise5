using System;
using System.Collections.Generic;
using System.Text;
using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    internal class Hangar(string name, int capacity) : Garage(name, capacity)
    {
        public override VehicleType[] VehicleTypes => [VehicleType.Airplane];
    }
}
