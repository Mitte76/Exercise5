using System;
using System.Collections.Generic;
using Exercise5.Data;
using Exercise5.Facilities;
using Exercise5.Vehicles;

namespace Exercise5.Menus
{
    internal class MenuFactory
    {
        private readonly List<Garage> _facilities;

        public MenuFactory(List<Garage> facilities)
        {
            _facilities = facilities ?? new List<Garage>();
        }

        public Menu CreateMainMenu()
        {
            return new Menu("Main Menu", GetMainMenuItems, isMainMenu: true);
        }

        private Menu OpenFacility()
        {
            return new Menu("Available Facilities", GetAvailableFacilities);
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
            return new Menu($"Garage: {garage.Name}", () => GarageSubMenus(garage));
        }

        private MenuItem[] GarageSubMenus(Garage garage)
        {
            var list = new List<MenuItem>
            {
                new MenuItem("Check Availability", action: () => garage.PrintAvailableSlots()),
                new MenuItem("Park Car", action: () => ParkCar(garage)),
                new MenuItem("Back"),
            };

            return list.ToArray();
        }

        private static string? ParkCar(Garage garage)
        {
            try
            {
                Car car = CreateCar();
                string? message = garage.ParkCar(car);

                return message;
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }
        }

        private static Car CreateCar()
        {
            Console.Clear();

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

            return new Car(registrationNumber, color);
        }

        private MenuItem[] GetMainMenuItems()
        {
            var list = new List<MenuItem>();

            list.Add(new MenuItem("Add Facility", submenu: CreateFacilityMenu()));

            if (_facilities.Count > 0)
            {
                list.Add(new MenuItem("Select Facilitity", submenu: OpenFacility()));
            }

            list.Add(
                new MenuItem("Load/Save Data", submenu: new Menu("Main Menu", GetDatabaseOptions))
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
                    submenu: new Menu("Available backups", GetBackupMenuItems)
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
            return new Menu("Add Facility", GetFacilityMenuItems);
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
