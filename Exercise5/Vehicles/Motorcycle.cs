using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Motorcycle(int cylinderVolume, string color) : Vehicle(color)
    {
        public int CylinderVolume { get; set; } = cylinderVolume;
    }
}
