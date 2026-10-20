using Exercise5.Vehicles;

namespace Exercise5.Facilities
{
    internal class CarGarage(string name, int capacity) : Garage(name, capacity)
    {
        public override VehicleType[] VehicleTypes =>
            [VehicleType.Car, VehicleType.Motorcycle, VehicleType.Bus];
    }
}
