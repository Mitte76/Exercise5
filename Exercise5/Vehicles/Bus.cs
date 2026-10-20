using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Bus(string brand, int numberOfSeats, string color) : Vehicle(brand, color)
    {
        public int NumberOfSeats { get; set; } = numberOfSeats;
    }
}
