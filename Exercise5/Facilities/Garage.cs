using System;
using System.Linq;
using System.Text.Json.Serialization;
using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
    [JsonDerivedType(typeof(CarGarage), "CarGarage")]
    [JsonDerivedType(typeof(Hangar), "Hangar")]
    [JsonDerivedType(typeof(Marina), "Marina")]
    [JsonDerivedType(typeof(MCGarage), "MCGarage")]
    internal abstract class Garage(string name, int capacity)
    {
        public abstract VehicleType[] VehicleTypes { get; }
        public int Capacity => ParkingSpots.Length;
        private ParkingSpot[] parkingSpots = Enumerable
            .Range(0, capacity)
            .Select(_ => new ParkingSpot())
            .ToArray();

        [JsonInclude]
        public ParkingSpot[] ParkingSpots
        {
            get => parkingSpots;
            private set => parkingSpots = value;
        }
        public int OccupiedSlots => ParkingSpots.Count(p => p.IsOccupied);
        public int AvailableSlots => ParkingSpots.Count(p => !p.IsOccupied);

        public void GetFirstAvailableParkignSpot()
        {
            ParkingSpot? spot = parkingSpots.FirstOrDefault(p => !p.IsOccupied);
        }

        public ParkingSpot[] GetAllOccupiesSpots()
        {
            ParkingSpot[] occupiedSpots = [.. parkingSpots.Where(p => p.IsOccupied)];
            return occupiedSpots;
        }

        public abstract string? PrintAvailableSlots();

        public string Name { get; private set; } = name;

        public string ParkCar(Vehicle vehicle)
        {
            ParkingSpot? spot = parkingSpots.FirstOrDefault(p => !p.IsOccupied);
            if (spot == null)
                return "No available parking spot found";
            spot.AddVehicle(vehicle);
            return $"Car parked successfully {AvailableSlots} slots available. {OccupiedSlots} Vehicles currenty parked";
        }

        public void ResizeCapacity(int newCapacity)
        {
            if (OccupiedSlots > newCapacity)
            {
                throw new ArgumentException(
                    "new capacity is less than current occupants\n"
                        + $"You currently have {OccupiedSlots} parked vehicles."
                );
            }

            Array.Resize(ref parkingSpots, newCapacity);
        }
    }
}
