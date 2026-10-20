using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Airplane(string brand, int numberOfEngines, string color) : Vehicle(brand, color)
    {
        public int NumberOfEngines { get; set; } = numberOfEngines;
    }
}
