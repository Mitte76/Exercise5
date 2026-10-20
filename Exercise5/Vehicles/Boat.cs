using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Vehicles
{
    internal class Boat(string brand, decimal length, string color) : Vehicle(brand, color)
    {
        public decimal Length { get; set; } = length;
    }
}
