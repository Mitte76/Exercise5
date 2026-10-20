using System;
using System.Collections.Generic;
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

            foreach (var item in _facilities)
            {
                if (item is CarGarage garage)
                {
                    list.Add(new MenuItem($"{item.Name} (Garage)", submenu: GarageMenu(garage)));
                }
                if (item is Marina marina)
                {
                    list.Add(new MenuItem($"{item.Name} (Marina)", submenu: GarageMenu(marina)));
                }
            }
            list.Add(new MenuItem("Back"));

            return list.ToArray();
        }

        private Menu GarageMenu(Garage garage)
        {
            return new Menu(() => $"Garage: {garage.Name}", () => GarageSubMenus(garage));
        }

        private static MenuItem[] GarageSubMenus(Garage garage)
        {
            var list = new List<MenuItem>
            {
                new MenuItem("Check Availability", action: () => garage.PrintAvailableSlots()),
                new MenuItem("Get Car", submenu: GetCar(garage)),
                new MenuItem("Park Car", action: () => ParkCar(garage)),
                new MenuItem("Back"),
            };

            return [.. list];
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
                        new MenuItem($"{parkingSpot.Vehicle.Brand}", submenu: EditVehicle(vehicle))
                    );
                }
            }

            list.Add(new MenuItem("Back"));

            return [.. list];
        }

        private static Menu EditVehicle(Vehicle vehicle)
        {
            return new Menu(() => GetVehicleHeader(vehicle), () => EditVehicleSubMenu(vehicle));
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

                Boat boat =>
                [
                    new MenuItem(
                        "Change Length",
                        action: () => EditVehicleProperty(value => boat.Length = value, "Length")
                    ),
                ],

                Bus bus =>
                [
                    // Bussens specifika menyval
                ],

                Motorcycle motorcycle =>
                [
                    // Motorcykelns specifika menyval
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

        private string? CreateFacility(GarageType type)
        {
            switch (type)
            {
                case GarageType.CarGarage:
                    CreateCarGarage();
                    break;
                case GarageType.Marina:
                    CreateMarina();
                    break;
                case GarageType.Hangar:
                    break;
                case GarageType.MCGarage:
                    break;
            }
            return null;
        }

        private void CreateMarina()
        {
            Console.Clear();
            string? name;
            while (true)
            {
                name = GetFacilityName(GarageType.Marina);

                if (name == null)
                {
                    return;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine(
                    "How many parking spaces are there in the garage? (Empty row to abort)"
                );
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value > 0)
                {
                    _facilities.Add(new Marina(name, value));
                    break;
                }
                else if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                Console.WriteLine(
                    "Please enter a valid number of parking spaces greater than 0. (Empty row to abort)"
                );
            }
        }

        private void CreateCarGarage()
        {
            Console.Clear();
            string? name;
            while (true)
            {
                name = GetFacilityName(GarageType.CarGarage);

                if (name == null)
                {
                    return;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine(
                    "How many parking spaces are there in the garage? (Empty row to abort)"
                );
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value > 0)
                {
                    _facilities.Add(new CarGarage(name, value));
                    break;
                }
                else if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                Console.WriteLine(
                    "Please enter a valid number of parking spaces greater than 0. (Empty row to abort)"
                );
            }
        }

        private string? GetFacilityName(GarageType facilityType)
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
    }
}
