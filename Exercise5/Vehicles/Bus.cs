using System;

namespace Exercise5.Vehicles
{
    internal class Bus(string brand, string color, string licensePlateNumber, int numberOfSeats)
        : Vehicle(brand, color)
    {
        public string LicensePlateNumber
        {
            get;
            set => field = Car.ValidateLicensePlateNumber(value);
        } = Car.ValidateLicensePlateNumber(licensePlateNumber);

        public int NumberOfSeats { get; set; } = numberOfSeats;

        public static Bus Create()
        {
            var (brand, color) = GetCommonProperties("Bus");

            string? licensePlate = ReadInput("Enter license plate (e.g. ABC123 or ABC12D):");
            if (licensePlate == null)
                throw new OperationCanceledException("Adding Bus aborted");

            string? amountIn = ReadInput("Enter amount of seats:");
            if (amountIn == null)
                throw new OperationCanceledException("Adding Bus aborted");

            if (!int.TryParse(amountIn, out int amount) || amount <= 0)
                throw new ArgumentException("Number of seats must be a positive number.");

            return new Bus(brand, color, licensePlate, amount);
        }
    }
}
