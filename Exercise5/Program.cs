using System.Numerics;
using Exercise5.Facilities;
using Exercise5.Menus;

namespace Exercise5
{
    internal class Program
    {
        static void Main()
        {
            Menu menu = new(
                "MainMenu:",
                [
                    new MenuItem(
                        "Create facility",
                        submenu: new Menu(
                            "Create facility:",
                            [
                                new MenuItem(
                                    "Create garage",
                                    action: () => CreateFacility(FacilityType.Garage)
                                ),
                                new MenuItem("Back"),
                            ]
                        )
                    ),
                    new MenuItem(
                        "Create facility2",
                        submenu: new Menu(
                            "Create facility:",
                            [new MenuItem("Test2"), new MenuItem("Back")]
                        )
                    ),
                    new MenuItem(
                        "Exit",
                        action: () =>
                        {
                            Console.WriteLine("Bye!");
                        }
                    ),
                ],
                isMainMenu: true
            );

            menu.ShowMenu();
        }

        static void CreateFacility(FacilityType type)
        {
            Console.WriteLine("How many spaces in tha garage?");
            string? spaces = Console.ReadLine();
        }
    }
}
