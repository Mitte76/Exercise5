using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Exercise5.Vehicles
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(Airplane), "Airplane")]
    [JsonDerivedType(typeof(Boat), "Boat")]
    [JsonDerivedType(typeof(Bus), "Bus")]
    [JsonDerivedType(typeof(Car), "Car")]
    [JsonDerivedType(typeof(Motorcycle), "Motorcycle")]
    public class Vehicle(string color)
    {
        public int Weight { get; set; }
        public int Seats { get; set; }
        public string Color { get; set; } = IsValidColor(color);

        private static string IsValidColor(string color)
        {
            if (!Regex.IsMatch(color, @"^[\p{L}]+$"))
                throw new ArgumentException("Invalid color");
            return color;
        }
    }
}
