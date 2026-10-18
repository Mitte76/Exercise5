using System;
using System.Collections.Generic;
using System.Text;
using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    internal class Garage(int parkingSpots)
    {
        private Vehicle[] vehicles = new Vehicle[parkingSpots];

        public int ParkingSpots { get; private set; } = parkingSpots;

        public int OccupiedSpots => vehicles.Count(v => v != null);

        public void ResizeCapacity(int newCapacity)
        {
            if (OccupiedSpots > newCapacity)
            {
                throw new ArgumentException(
                    "new capacity is less than current occupants\n"
                        + $"You currently have {OccupiedSpots} parked vehicles."
                );
            }

            Array.Resize(ref vehicles, newCapacity);
        }
    }
}
