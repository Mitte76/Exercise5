using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    public class ParkingSpot
    {
        public bool IsOccupied => Vehicle != null;
        private Vehicle? Vehicle { get; set; }

        public void AddVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
