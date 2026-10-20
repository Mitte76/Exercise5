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
    public class Vehicle(string brand, string color)
    {
        //public int Weight { get; set; }
        //public int Seats { get; set; }
        public string Color { get; set; } = IsValidInput(color, "Color");
        public string Brand
        {
            get;
            set => field = IsValidInput(value, "Brand");
        } = IsValidInput(brand, "Brand");

        private static string IsValidInput(string color, string property)
        {
            if (!Regex.IsMatch(color, @"^[\p{L}]+$"))
                throw new ArgumentException($"Invalid {property}");
            return color;
        }

        protected static (string Brand, string Color) GetCommonProperties(string type)
        {
            string? brand = ReadInput("Enter brand:");
            if (brand == null)
                throw new OperationCanceledException($"Adding {type} aborted");

            string? color = ReadInput("Enter color:");
            if (color == null)
                throw new OperationCanceledException($"Adding {type} aborted");

            return (brand, color);
        }

        protected static string? ReadInput(string prompt)
        {
            Console.WriteLine($"{prompt} Leave empty to exit");
            string? input = Console.ReadLine();

            return string.IsNullOrWhiteSpace(input) ? null : input;
        }
    }
}
