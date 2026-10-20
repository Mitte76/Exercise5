using System;
using System.Text.RegularExpressions;

namespace Exercise5.Vehicles
{
    [Serializable()]
    internal class Car(string brand, string color, string licensePlateNumber)
        : Vehicle(brand, color)
    {
        public string LicensePlateNumber
        {
            get;
            set => field = ValidateLicensePlateNumber(value);
        } = ValidateLicensePlateNumber(licensePlateNumber);

        public static string ValidateLicensePlateNumber(string registratíonNumber)
        {
            string pattern = @"^[A-HJ-PR-UW-Z]{3}\s?[0-9]{2}[A-HJ-PR-UW-Z0-9]$";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(registratíonNumber) || !regex.IsMatch(registratíonNumber))
                throw new ArgumentException("Licence plate number is not valid");

            return registratíonNumber.ToUpper();
        }

        public static Car Create()
        {
            var (brand, color) = GetCommonProperties("Car");

            string? licensePlate = ReadInput("Enter license plate (e.g. ABC123 or ABC12D):");
            if (licensePlate == null)
                throw new OperationCanceledException("Adding Car Aborted");

            return new Car(brand, color, licensePlate);
        }
    }
}
