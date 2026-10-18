using Exercise5.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Garage
{
    internal class Garage
    {
        private Vehicle[] vehicles;

        public int ParkingSpots { get; private set; }

        public Garage(int parkingSpots)
        {
            ParkingSpots = parkingSpots;
            vehicles = new Vehicle[parkingSpots];
        }

        public int OccupiedSpots => vehicles.Count(v => v != null);
        public void ResizeCapacity(int newCapacity)
        {
            if (OccupiedSpots > newCapacity)
            {
                throw new ArgumentException("new capacity is less than current occupants\n" +
                    $"You currently have {OccupiedSpots} parked vehicles.");
            }

            Array.Resize(ref vehicles, newCapacity);
        }

    }
}
