using System;
using System.Text.Json.Serialization;
using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    public class ParkingSpot
    {
        public bool IsOccupied => Vehicle != null;

        [JsonInclude]
        public Vehicle? Vehicle { get; private set; }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
