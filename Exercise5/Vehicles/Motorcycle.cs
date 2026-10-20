using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Motorcycle(string brand, string color, int cylinderVolume)
        : Vehicle(brand, color)
    {
        public int CylinderVolume { get; set; } = cylinderVolume;
    }
}
