using System;

namespace Exercise5.Vehicles
{
    internal class Motorcycle(
        string brand,
        string color,
        string licensePlateNumber,
        int cylinderVolume
    ) : Vehicle(brand, color)
    {
        public string LicensePlateNumber
        {
            get;
            set => field = Car.ValidateLicensePlateNumber(value);
        } = Car.ValidateLicensePlateNumber(licensePlateNumber);

        public int CylinderVolume { get; set; } = cylinderVolume;

        public static Motorcycle Create()
        {
            var (brand, color) = GetCommonProperties("Motorcycle");

            string? licensePlate = ReadInput("Enter license plate (e.g. ABC123 or ABC12D):");
            if (licensePlate == null)
                throw new OperationCanceledException("Adding Motorcycle aborted");

            string? cylinderVolumeInput = ReadInput("Enter engine volume (cc):");
            if (cylinderVolumeInput == null)
                throw new OperationCanceledException("Adding Motorcycle aborted");

            if (!int.TryParse(cylinderVolumeInput, out int cylinderVolume) || cylinderVolume <= 0)
                throw new ArgumentException("Engine volume must be a positive number.");

            return new Motorcycle(brand, color, licensePlate, cylinderVolume);
        }
    }
}
