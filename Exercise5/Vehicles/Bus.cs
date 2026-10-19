using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Bus(int numberOfSeats, string color) : Vehicle(color)
    {
        public int NumberOfSeats { get; set; } = numberOfSeats;
    }
}
