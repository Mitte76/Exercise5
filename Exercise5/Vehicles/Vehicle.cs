using System;

namespace Exercise5.Vehicles
{
    public class Vehicle(string color)
    {
        private int Weight { get; set; }
        private int Seats { get; set; }
        private string Color { get; set; } = color;
    }
}
