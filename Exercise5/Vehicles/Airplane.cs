using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Airplane(int numberOfEngines, string color) : Vehicle(color)
    {
        public int NumberOfEngines { get; set; } = numberOfEngines;
    }
}
