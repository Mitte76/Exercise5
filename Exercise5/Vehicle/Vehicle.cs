using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicle
{
    public class Vehicle(string color)
    {
        private int Weight { get; set; }
        private int Seats { get; set; }
        private string Color { get; set; } = color;
    }
}
