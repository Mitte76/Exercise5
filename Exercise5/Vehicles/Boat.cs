using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Boat(decimal length, string color) : Vehicle(color)
    {
        public decimal Length { get; set; } = length;
    }
}
