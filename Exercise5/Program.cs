using System;
using System.Collections.Generic;
using System.Text;
using Exercise5.Facilities;
using Exercise5.Menus;

namespace Exercise5
{
    internal class Program
    {
        private static List<Facility> facilities = new List<Facility>();

        static void Main()
        {
            Menu menu = CreateMainMenu();
            menu.ShowMenu();
        }

        static Menu CreateMainMenu()
        {
            return new Menu("Main Menu", GetMainMenuItems, isMainMenu: true);
        }

        static MenuItem[] GetMainMenuItems()
        {
            List<MenuItem> list = new List<MenuItem>();

            list.Add(new MenuItem("Add Facility", submenu: CreateFacilityMenu()));

            if (facilities.Count > 0)
            {
                list.Add(new MenuItem("Open Facility", submenu: OpenFacility()));
            }

            list.Add(new MenuItem("Exit"));
            return [.. list];
        }

        static Menu CreateFacilityMenu()
        {
            return new Menu("Add Facility", GetFacilityMenuItems);
        }

        static Menu OpenFacility()
        {
            return new Menu("Available Facilities", GetAvailableFacilities);
        }

        static MenuItem[] GetFacilityMenuItems()
        {
            return
            [
                new MenuItem("Add Garage", action: () => CreateFacility(FacilityType.Garage)),
                new MenuItem("Add Hangar", action: () => CreateFacility(FacilityType.Hangar)),
                new MenuItem("Add Marina", action: () => CreateFacility(FacilityType.Marina)),
                new MenuItem("Add Mc garage", action: () => CreateFacility(FacilityType.MCGarage)),
                new MenuItem("Back"),
            ];
        }

        static MenuItem[] GetAvailableFacilities()
        {
            List<MenuItem> list = [];

            foreach (var item in facilities)
            {
                if (item is Garage garage)
                {
                    list.Add(
                        new MenuItem($"{item.Name} (Garage)", submenu: GarageMenu(garage: garage))
                    );
                }
                if (item is Marina marina)
                {
                    list.Add(
                        new MenuItem($"{item.Name} (Marina)", submenu: GarageMenu(garage: marina))
                    );
                }
            }
            list.Add(new MenuItem("Back"));

            return [.. list];
        }

        static Menu GarageMenu(Facility garage)
        {
            return new Menu($"{garage.Name}:", () => GarageSubMenus(garage));
        }

        static MenuItem[] GarageSubMenus(Facility garage)
        {
            List<MenuItem> list = [];

            list.Add(
                new MenuItem("Check Availability", action: () => garage.PrintAvailableSlots())
            );
            list.Add(new MenuItem("Back"));

            return [.. list];
        }

        static Menu NotImplementedYetMenu()
        {
            return new Menu("Not ImplementedYet:", NotImplementedYeSubtMenu);
        }

        static MenuItem[] NotImplementedYeSubtMenu()
        {
            return new MenuItem[] { new MenuItem("Back") };
        }

        static void CreateFacility(FacilityType type)
        {
            switch (type)
            {
                case FacilityType.Garage:
                    CreateGarage();
                    break;
                case FacilityType.Marina:
                    CreateMarina();
                    break;
                case FacilityType.Hangar:
                    break;
                case FacilityType.MCGarage:
                    break;
            }
        }

        static void CreateMarina()
        {
            Console.Clear();
            string? name;
            while (true)
            {
                name = GetFacilityName(FacilityType.Marina);

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
                    facilities.Add(new Marina(name, value));
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

        static void CreateGarage()
        {
            Console.Clear();
            string? name;
            while (true)
            {
                name = GetFacilityName(FacilityType.Garage);

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
                    facilities.Add(new Garage(name, value));
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

        private static string? GetFacilityName(FacilityType facilityType)
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
