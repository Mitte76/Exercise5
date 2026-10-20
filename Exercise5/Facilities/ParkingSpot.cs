using System;
using System.Text.Json.Serialization;
using System.Threading;
using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    public class ParkingSpot
    {
        private static int nextId;

        public int Id { get; } = Interlocked.Increment(ref nextId);

        public bool IsOccupied => Vehicle != null;

        [JsonInclude]
        public Vehicle? Vehicle { get; private set; }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
