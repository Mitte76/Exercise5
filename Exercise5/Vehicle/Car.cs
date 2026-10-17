using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Exercise5.Vehicle
{
    public class Car (string registreringsnummer, string color) : Vehicle(color)
    {
        public string RegistreringsNummer { get; private set; } = registreringsnummer;

        private static string ValidateRegistrationNumber(string registratíonNumber)
        {
            string pattern = @"^[A-HJ-PR-UW-Z]{3}\s?[0-9]{2}[A-HJ-PR-UW-Z0-9]$";
            Regex regex = new (pattern, RegexOptions.IgnoreCase);

            if (string.IsNullOrEmpty(registratíonNumber) || !regex.IsMatch(registratíonNumber))
                throw new ArgumentException("registration number is not valid");

            return registratíonNumber;
        }


    }





}
