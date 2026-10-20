using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exercise5.Data;
using Exercise5.Facilities;
using Exercise5.Vehicles;

namespace Exercise5.Menus
{
    internal class MenuFactory(List<Garage> facilities)
    {
        private readonly List<Garage> _facilities = facilities ?? [];

        public Menu CreateMainMenu()
        {
            return new Menu(() => "Main Menu", GetMainMenuItems, isMainMenu: true);
        }

        private Menu OpenFacility()
        {
            return new Menu(() => "Available Facilities", GetAvailableFacilities);
        }

        private MenuItem[] GetAvailableFacilities()
        {
            var list = new List<MenuItem>();

            foreach (var facility in _facilities)
            {
                list.Add(
                    new MenuItem(
                        $"{facility.Name} ({facility.GetType().Name})",
                        submenu: FacilityMenu(facility)
                    )
                );
            }

            list.Add(new MenuItem("Back"));

            return [.. list];
        }

        private static Menu FacilityMenu(Garage garage)
        {
            return new Menu(() => $"Garage: {garage.Name}", () => GarageSubMenus(garage));
        }

        private static MenuItem[] GarageSubMenus(Garage garage)
        {
            string vehicleTypes = string.Join(" / ", garage.VehicleTypes);

            return
            [
                new MenuItem("Check Availability", action: () => garage.PrintAvailableSlots()),
                new MenuItem($"Get {vehicleTypes}", submenu: GetVehicle(garage)),
                new MenuItem(
                    $"Park {string.Join(" / ", garage.VehicleTypes)}",
                    submenu: ParkVehicle(garage)
                ),
                new MenuItem("Back"),
            ];
        }

        private static Menu GetVehicle(Garage garage)
        {
            return new Menu(
                () => "Select Vehicle",
                () =>
                {
                    var vehicles = new List<MenuItem>();

                    foreach (ParkingSpot spot in garage.ParkingSpots)
                    {
                        if (spot.Vehicle is not Vehicle vehicle)
                            continue;

                        bool isAllowed = vehicle switch
                        {
                            Car => garage.VehicleTypes.Contains(VehicleType.Car),
                            Bus => garage.VehicleTypes.Contains(VehicleType.Bus),
                            Motorcycle => garage.VehicleTypes.Contains(VehicleType.Motorcycle),
                            Boat => garage.VehicleTypes.Contains(VehicleType.Boat),
                            Airplane => garage.VehicleTypes.Contains(VehicleType.Airplane),
                            _ => false,
                        };

                        if (!isAllowed)
                            continue;

                        vehicles.Add(
                            new MenuItem(
                                $"{vehicle.Brand} ({vehicle.GetType().Name})",
                                submenu: EditVehicle(spot)
                            )
                        );
                    }

                    return [.. vehicles, new MenuItem("Back")];
                }
            );
        }

        private static Menu ParkVehicle(Garage garage)
        {
            return new Menu(
                () => "Select vehicle type",
                () =>
                    [
                        .. garage.VehicleTypes.Select(type => new MenuItem(
                            type.ToString(),
                            action: () => ParkVehicle(garage, type)
                        )),
                        new MenuItem("Back"),
                    ]
            );
        }

        private static string? ParkVehicle(Garage garage, VehicleType vehicleType)
        {
            try
            {
                Vehicle? vehicle = GetNewVehicle(vehicleType);

                if (vehicle == null)
                    return "Parking aborted";

                return garage.ParkCar(vehicle);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        //private static string? ParkVehicle(Garage garage)
        //{
        //    try
        //    {
        //        Vehicle? vehicle = GetNewVehicle(garage.VehicleType);
        //        return garage.ParkCar(vehicle);
        //    }
        //    catch (Exception e)
        //    {
        //        return e.Message;
        //    }
        //}

        private static Vehicle? GetNewVehicle(VehicleType vehicleType)
        {
            switch (vehicleType)
            {
                case VehicleType.Airplane:
                    return Airplane.Create();
                case VehicleType.Boat:
                    return Boat.Create();
                case VehicleType.Bus:
                    return Bus.Create();
                case VehicleType.Car:
                    return Car.Create();
                case VehicleType.Motorcycle:
                    return Motorcycle.Create();
                default:
                    return null;
            }
        }

        private static Menu GetCar(Garage garage)
        {
            return new Menu(() => "Find Car", () => GetCarSubMenu(garage));
        }

        private static MenuItem[] GetCarSubMenu(Garage garage)
        {
            var list = new List<MenuItem>
            {
                new MenuItem("Choose from List", submenu: ListCars(garage)),
                new MenuItem("Back"),
            };

            return [.. list];
        }

        private static Menu ListCars(Garage garage)
        {
            return new Menu(() => "Choose Car", () => ListCarSubMenu(garage));
        }

        private static MenuItem[] ListCarSubMenu(Garage garage)
        {
            var list = new List<MenuItem>();

            ParkingSpot[] occupiedSpots = garage.GetAllOccupiesSpots();

            foreach (ParkingSpot parkingSpot in occupiedSpots)
            {
                if (parkingSpot.Vehicle is Vehicle vehicle)
                {
                    list.Add(
                        new MenuItem(
                            $"{parkingSpot.Vehicle.Brand}",
                            submenu: EditVehicle(parkingSpot)
                        )
                    );
                }
            }

            list.Add(new MenuItem("Back"));

            return [.. list];
        }

        private static Menu EditVehicle(ParkingSpot parkingSpot)
        {
            return new Menu(
                () => GetVehicleHeader(parkingSpot.Vehicle!),
                () => EditVehicleSubMenu(parkingSpot.Vehicle!),
                extra: () => GetStats(parkingSpot)
            );
        }

        private static string GetStats(ParkingSpot parkingSpot)
        {
            StringBuilder sb = new StringBuilder();
            switch (parkingSpot.Vehicle)
            {
                case Car car:
                    sb.AppendLine($"Brand: {car.Brand}");
                    sb.AppendLine($"License plate: {car.LicensePlateNumber}");
                    sb.AppendLine($"Color: {car.Color}");
                    sb.Append($"ParkingSpot nr: {parkingSpot.Id}");
                    break;

                case Bus bus:
                    sb.AppendLine($"Brand: {bus.Brand}");
                    sb.AppendLine($"License plate: {bus.LicensePlateNumber}");
                    sb.AppendLine($"Color: {bus.Color}");
                    sb.AppendLine($"Seats: {bus.NumberOfSeats}");
                    sb.Append($"ParkingSpot nr: {parkingSpot.Id}");
                    break;

                case Motorcycle motorcycle:
                    sb.AppendLine($"Brand: {motorcycle.Brand}");
                    sb.AppendLine($"License plate: {motorcycle.LicensePlateNumber}");
                    sb.AppendLine($"Engine volume: {motorcycle.CylinderVolume}");
                    sb.AppendLine($"Color: {motorcycle.Color}");
                    sb.Append($"ParkingSpot nr: {parkingSpot.Id}");
                    break;

                case Boat boat:
                    sb.AppendLine($"Brand: {boat.Brand}");
                    sb.AppendLine($"Length: {boat.Length}");
                    sb.AppendLine($"Color: {boat.Color}");
                    sb.Append($"ParkingSpot nr: {parkingSpot.Id}");
                    break;

                case Airplane airplane:
                    sb.AppendLine($"Brand: {airplane.Brand}");
                    sb.AppendLine($"Number of engines: {airplane.NumberOfEngines}");
                    sb.AppendLine($"Color: {airplane.Color}");
                    sb.Append($"ParkingSpot nr: {parkingSpot.Id}");
                    break;
            }

            return sb.ToString();
        }

        private static string GetVehicleHeader(Vehicle vehicle)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{vehicle.Brand} ");

            switch (vehicle)
            {
                case Car car:
                    sb.Append($"{car.LicensePlateNumber} ");
                    break;
                case Motorcycle motorcycle:
                    sb.Append($"{motorcycle.LicensePlateNumber} ");
                    break;
                case Bus bus:
                    sb.Append($"{bus.LicensePlateNumber} ");
                    break;
                case Boat boat:
                    sb.Append($"{boat.Length} ");
                    break;
                case Airplane airplane:
                    sb.Append($"{airplane.NumberOfEngines} ");
                    break;
            }

            return sb.ToString().TrimEnd();
        }

        private static MenuItem[] EditVehicleSubMenu(Vehicle vehicle)
        {
            return
            [
                new MenuItem(
                    "Change Brand",
                    action: () => EditVehicleProperty(value => vehicle.Brand = value, "brand name")
                ),
                new MenuItem(
                    "Change Color",
                    action: () => EditVehicleProperty(value => vehicle.Color = value, "color")
                ),
                .. GetVehicleSpecificMenuItems(vehicle),
                new MenuItem("Back"),
            ];
        }

        private static MenuItem[] GetVehicleSpecificMenuItems(Vehicle vehicle)
        {
            return vehicle switch
            {
                Car car =>
                [
                    new MenuItem(
                        "Change License Plate Number",
                        action: () =>
                            EditVehicleProperty(
                                value => car.LicensePlateNumber = value,
                                "license plate number"
                            )
                    ),
                ],
                Bus bus =>
                [
                    new MenuItem(
                        "Change License Plate Number",
                        action: () =>
                            EditVehicleProperty(
                                value => bus.LicensePlateNumber = value,
                                "license plate number"
                            )
                    ),
                    new MenuItem(
                        "Change number of seats",
                        action: () =>
                            EditVehicleProperty(
                                value => bus.NumberOfSeats = value,
                                "number of seats"
                            )
                    ),
                ],
                Motorcycle motorcycle =>
                [
                    new MenuItem(
                        "Change License Plate Number",
                        action: () =>
                            EditVehicleProperty(
                                value => motorcycle.LicensePlateNumber = value,
                                "license plate number"
                            )
                    ),
                    new MenuItem(
                        "Change engine volume",
                        action: () =>
                            EditVehicleProperty(
                                value => motorcycle.CylinderVolume = value,
                                "engine volume"
                            )
                    ),
                ],

                Boat boat =>
                [
                    new MenuItem(
                        "Change Length",
                        action: () => EditVehicleProperty(value => boat.Length = value, "Length")
                    ),
                ],

                Airplane airplane =>
                [
                    // Flygplanets specifika menyval
                ],

                _ => [],
            };
        }

        private static string? EditVehicleProperty(Action<int> update, string propertyName)
        {
            Console.WriteLine($"Enter new {propertyName}. Leave empty to exit");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                return $"Changing {propertyName} aborted";

            if (!int.TryParse(input, out int value))
                return $"Invalid {propertyName}. Please enter a number.";

            try
            {
                update(value);
                return $"{propertyName} updated successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static string? EditVehicleProperty(Action<string> update, string propertyName)
        {
            Console.WriteLine($"Enter new {propertyName}. Leave empty to exit");
            string? value = Console.ReadLine();

            if (string.IsNullOrEmpty(value))
                return $"Changing {propertyName} aborted";

            try
            {
                update(value);
                return $"{propertyName} updated successfully";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static string? ShowAllCars(Garage garage)
        {
            ParkingSpot[] occupiedSpots = garage.GetAllOccupiesSpots();

            foreach (ParkingSpot parkingSpot in occupiedSpots)
            {
                if (parkingSpot.Vehicle is Car car)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"ParkingSpot: {parkingSpot.Id}");
                    sb.AppendLine($"Brand: {car.Brand}");
                    sb.AppendLine($"License Plate Number: {car.LicensePlateNumber.ToUpper()}");
                    Console.WriteLine(sb.ToString());
                }
            }

            Console.ReadKey();
            return null;
        }

        private static string? ParkCar(Garage garage)
        {
            try
            {
                Car car = CreateCar();
                string? message = garage.ParkCar(car);

                return message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static Car CreateCar()
        {
            Console.Clear();

            Console.WriteLine("What is the car's brand? (e.g. Volvo or Ford) Leave empty to exit");
            string? brand = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(brand))
                throw new OperationCanceledException("Parking Car Aborted");

            Console.WriteLine(
                "What is the car's registration number? (e.g. ABC123 or ABC12D) Leave empty to exit"
            );
            string? registrationNumber = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(registrationNumber))
                throw new OperationCanceledException("Parking Car Aborted");

            Console.WriteLine("What color is the car? Leave empty to exit");
            string? color = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(color))
                throw new OperationCanceledException("Parking Car Aborted");

            return new Car(brand, registrationNumber, color);
        }

        private MenuItem[] GetMainMenuItems()
        {
            var list = new List<MenuItem>();

            if (_facilities.Count > 0)
            {
                list.Add(new MenuItem("Open Facilitity", submenu: OpenFacility()));
            }

            list.Add(new MenuItem("Add Facility", submenu: CreateFacilityMenu()));

            list.Add(
                new MenuItem(
                    "Load/Save Data",
                    submenu: new Menu(() => "Main Menu", GetDatabaseOptions)
                )
            );

            list.Add(new MenuItem("Exit"));
            return list.ToArray();
        }

        private MenuItem[] GetDatabaseOptions()
        {
            var list = new List<MenuItem>
            {
                new MenuItem(
                    "Load from File",
                    submenu: new Menu(() => "Available backups", GetBackupMenuItems)
                ),
                new MenuItem(
                    "Save to File",
                    taskAction: () => DataHandler.WriteToDatabase(_facilities)
                ),
                new MenuItem("Back"),
            };
            return [.. list];
        }

        private MenuItem[] GetBackupMenuItems()
        {
            string[] availableFiles = DataHandler.GetAvailableBackups();
            var items = new List<MenuItem>();
            foreach (var item in availableFiles)
            {
                items.Add(
                    new MenuItem(
                        item,
                        taskAction: () => DataHandler.ReadFromDatabase(_facilities, item)
                    )
                );
            }
            items.Add(new MenuItem("Back"));
            return [.. items];
        }

        private Menu CreateFacilityMenu()
        {
            return new Menu(() => "Add Facility", GetFacilityMenuItems);
        }

        private MenuItem[] GetFacilityMenuItems()
        {
            return
            [
                new MenuItem("Add Garage", action: () => CreateFacility(GarageType.CarGarage)),
                new MenuItem("Add Hangar", action: () => CreateFacility(GarageType.Hangar)),
                new MenuItem("Add Marina", action: () => CreateFacility(GarageType.Marina)),
                new MenuItem("Add Mc garage", action: () => CreateFacility(GarageType.MCGarage)),
                new MenuItem("Back"),
            ];
        }

        //private string? CreateFacility(GarageType type)
        //{
        //    switch (type)
        //    {
        //        case GarageType.CarGarage:
        //            CreateCarGarage();
        //            break;
        //        case GarageType.Marina:
        //            CreateMarina();
        //            break;
        //        case GarageType.Hangar:
        //            break;
        //        case GarageType.MCGarage:
        //            break;
        //    }
        //    return null;
        //}

        //private void CreateMarina()
        //{
        //    Console.Clear();
        //    string? name;
        //    while (true)
        //    {
        //        name = GetFacilityName(GarageType.Marina);

        //        if (name == null)
        //        {
        //            return;
        //        }
        //        break;
        //    }

        //    while (true)
        //    {
        //        Console.WriteLine(
        //            "How many parking spaces are there in the garage? (Empty row to abort)"
        //        );
        //        string? input = Console.ReadLine();

        //        if (int.TryParse(input, out int value) && value > 0)
        //        {
        //            _facilities.Add(new Marina(name, value));
        //            break;
        //        }
        //        else if (string.IsNullOrEmpty(input))
        //        {
        //            break;
        //        }

        //        Console.WriteLine(
        //            "Please enter a valid number of parking spaces greater than 0. (Empty row to abort)"
        //        );
        //    }
        //}

        private string? CreateFacility(GarageType garageType)
        {
            Console.Clear();

            string? name = GetFacilityName(garageType);

            if (name == null)
                return $"Adding {garageType} aborted";

            int? capacity = GetFacilityCapacity();

            if (capacity == null)
                return $"Adding {garageType} aborted";

            Garage facility = garageType switch
            {
                GarageType.CarGarage => new CarGarage(name, capacity.Value),
                GarageType.MCGarage => new MCGarage(name, capacity.Value),
                GarageType.Hangar => new Hangar(name, capacity.Value),
                GarageType.Marina => new Marina(name, capacity.Value),
                _ => throw new ArgumentOutOfRangeException(nameof(garageType)),
            };

            _facilities.Add(facility);

            return $"Created new {garageType} successfully";
        }

        private static string? GetFacilityName(GarageType facilityType)
        {
            while (true)
            {
                Console.WriteLine(
                    $"Please enter a name for your {facilityType}. (Empty row to abort)"
                );
                string? input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                return null;
            }
        }

        private static int? GetFacilityCapacity()
        {
            while (true)
            {
                Console.WriteLine("How many spaces should the facility have? (Empty row to abort)");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    return null;

                if (int.TryParse(input, out int value) && value > 0)
                    return value;

                Console.WriteLine("Please enter a valid number greater than 0.");
            }
        }
    }
}
