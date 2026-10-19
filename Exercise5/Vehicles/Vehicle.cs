using System;
using System.Text.RegularExpressions;

namespace Exercise5.Vehicles
{
    public class Vehicle(string color)
    {
        private int Weight { get; set; }
        private int Seats { get; set; }
        private string Color { get; set; } = IsValidColor(color);

        private static string IsValidColor(string color)
        {
            if (!Regex.IsMatch(color, @"^[\p{L}]+$"))
                throw new ArgumentException("Invalid color");
            return color;
        }
    }
}
