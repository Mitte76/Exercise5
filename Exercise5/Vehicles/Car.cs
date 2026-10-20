using System;
using System.Text.RegularExpressions;

namespace Exercise5.Vehicles
{
    [Serializable()]
    public class Car(string brand, string licensePlateNumber, string color) : Vehicle(brand, color)
    {
        public string LicensePlateNumber { get; set; } =
            ValidateLicensePlateNumber(licensePlateNumber);

        public static string ValidateLicensePlateNumber(string registratíonNumber)
        {
            string pattern = @"^[A-HJ-PR-UW-Z]{3}\s?[0-9]{2}[A-HJ-PR-UW-Z0-9]$";
            Regex regex = new(pattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(registratíonNumber) || !regex.IsMatch(registratíonNumber))
                throw new ArgumentException("registration number is not valid");

            return registratíonNumber.ToUpper();
        }
    }
}
