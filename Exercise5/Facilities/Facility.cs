using System;
using System.Linq;

namespace Exercise5.Facilities
{
    internal abstract class Facility(string name, int parkingSpots)
    {
        private ParkingSpot[] parkingSpots = Enumerable
            .Range(0, parkingSpots)
            .Select(_ => new ParkingSpot())
            .ToArray();

        public int OccupiedSlots => parkingSpots.Count(p => p.IsOcupied);
        public int AvailableSlots => parkingSpots.Count(p => !p.IsOcupied);

        public abstract void PrintAvailableSlots();

        public string Name { get; private set; } = name;

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
